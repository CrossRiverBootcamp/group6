using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Services;
using CustomerAccount.Services.Extensions;
using CustomerAccount.WebAPI.Middlewares;
using CustomerAccount.Services.options;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NServiceBus;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot configuration = new
            ConfigurationBuilder().AddJsonFile("appsettings.json",
            optional: false, reloadOnChange: true).Build();

string rabbitMQConnection = builder.Configuration.GetConnectionString("RabbitMQConnection");

#region back-end-use-nservicebus
builder.Host.UseNServiceBus(hostBuilderContext =>
{
    var endpointConfiguration = new EndpointConfiguration("CustomerAccount");
    endpointConfiguration.EnableInstallers();
    endpointConfiguration.EnableOutbox();

    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    persistence.ConnectionBuilder(
        connectionBuilder: () =>
        {
            return new SqlConnection(builder.Configuration.GetConnectionString("CustomerAccountNSBConnectionMiri"));
        });

    var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
    var subscriptions = persistence.SubscriptionSettings();
    subscriptions.CacheFor(TimeSpan.FromMinutes(1));
    dialect.Schema("nsb");

    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString(rabbitMQConnection);
    transport.UseConventionalRoutingTopology(QueueType.Quorum);
    var conventions = endpointConfiguration.Conventions();
    conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");
    conventions.DefiningEventsAs(type => type.Namespace == "Messages.Events");
    return endpointConfiguration;
});
#endregion


builder.Host.UseSerilog();
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));
// Add services to the container.
builder.Services.AddServiceExtension(builder.Configuration.GetConnectionString("CustomerAccountConnectionMiri"));
builder.Services.AddScoped<IAccountService, AccountService>(); 
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();
builder.Services.AddScoped<IOperationsHistoryService, OperationsHistoryService>();
builder.Services.AddScoped<IEmailVerificationService, EmailVerificationService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CrossRiverBank", Version = "v1" });

    // To Enable authorization using Swagger (JWT)    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
});


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
app.UseCors(options => {
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
