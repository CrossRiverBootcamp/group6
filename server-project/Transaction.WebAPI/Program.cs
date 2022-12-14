using Messages.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NServiceBus;
using Serilog;
using System.Data.SqlClient;
using System.Text;
using Transaction.Services.Extension;
using Transaction.Services.Interfaces;
using Transaction.Services.Services;
using Transaction.WebAPI.Extensions;
using Transaction.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

IConfigurationRoot configuration = new
            ConfigurationBuilder().AddJsonFile("appsettings.json",
            optional: false, reloadOnChange: true).Build();

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
    subscriptions.CacheFor(TimeSpan.FromMinutes(5));
    dialect.Schema("nsb");

    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString(rabbitMQConnection);
    transport.UseConventionalRoutingTopology(QueueType.Quorum);
    var routing = transport.Routing();
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
var key = Encoding.ASCII.GetBytes(configuration["JWT:key"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerSettings();
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration
            (configuration).CreateLogger();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHandlerErrorsMiddleware();
app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.MapControllers();

app.Run();
