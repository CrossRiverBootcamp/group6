using Messages.Commands;
using NServiceBus;
using System.Data.SqlClient;
using Transaction.Services.Extension;
using Transaction.Services.Interfaces;
using Transaction.Services.Services;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("TransactionConnectionMiri");
string connectionNSB = builder.Configuration.GetConnectionString("TransactionConnectionNSBMiri");
string rabbitMQConnection = builder.Configuration.GetConnectionString("RabbitMQConnection");

#region back-end-use-nservicebus
builder.Host.UseNServiceBus(hostBuilderContext =>
{
    var endpointConfiguration = new EndpointConfiguration("TransactionAPI");
    endpointConfiguration.EnableInstallers();
    endpointConfiguration.EnableOutbox();

    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    persistence.ConnectionBuilder(
        connectionBuilder: () =>
        {
            return new SqlConnection(connectionNSB);
        });

    var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
    var subscriptions = persistence.SubscriptionSettings();
    subscriptions.CacheFor(TimeSpan.FromMinutes(1));
    dialect.Schema("dbo");

    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString(rabbitMQConnection);
    transport.UseConventionalRoutingTopology(QueueType.Quorum);
    var routing = transport.Routing();
    //change to a veriable
    routing.RouteToEndpoint(typeof(UpdateBalance), destination: "CustomerAccount");

    var conventions = endpointConfiguration.Conventions();
    conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");
    conventions.DefiningEventsAs(type => type.Namespace == "Messages.Events");
    return endpointConfiguration;
});
#endregion
// Add services to the container.
builder.Services.AddServices(connection);
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => {
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
