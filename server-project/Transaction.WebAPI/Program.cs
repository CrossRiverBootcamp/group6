using Transaction.Services.Extension;
using Transaction.Services.Interfaces;
using Transaction.Services.Services;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("TransactionConnectionMiri");
string rabbitMQConnection = builder.Configuration.GetConnectionString("RabbitMQConnection");
#region back-end-use-nservicebus
/*builder.Host.UseNServiceBus(hostBuilderContext =>
{
    var endpointConfiguration = new EndpointConfiguration("TransactionAPi");
    endpointConfiguration.SendOnly();
    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString(rabbitMQConnection);
    transport.UseConventionalRoutingTopology(QueueType.Quorum);
    var conventions = endpointConfiguration.Conventions();
    conventions.DefiningEventsAs(type => type.Namespace == "Messages.Events");
    return endpointConfiguration;
});*/
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

app.UseAuthorization();

app.MapControllers();

app.Run();
