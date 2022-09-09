using CustomerAccount.Data.Entities;
using CustomerAccount.Services.Extensions;
using CustomerAccount.Services.Interfaces;
using CustomerAccount.Services.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Logging;

public class Program
{
    static ILog log = LogManager.GetLogger<Program>();

    public static ILog Log { get => log; set => log = value; }

    static async Task Main()
    {
        Console.Title = "CustomerAccount";

        var endpointConfiguration = new EndpointConfiguration("CustomerAccount");

        var databaseConnection = "Data Source=DESKTOP-411ES1J\\ADMIN;Initial Catalog=CustomerAccount;Integrated Security=True";
        var rabbitMQConnection = @"host=localhost";

        var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
        containerSettings.ServiceCollection.AddScoped<IAccountService, AccountService>();
        containerSettings.ServiceCollection.AddServiceExtension(databaseConnection);

        endpointConfiguration.EnableInstallers();
        endpointConfiguration.EnableOutbox();

        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(rabbitMQConnection);
        transport.UseConventionalRoutingTopology(QueueType.Quorum);

        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.ConnectionBuilder(
            connectionBuilder: () =>
            {
                return new SqlConnection(databaseConnection);
            });

        var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
        var subscriptions = persistence.SubscriptionSettings();
        subscriptions.CacheFor(TimeSpan.FromMinutes(1));
        dialect.Schema("dbo");

        var conventions = endpointConfiguration.Conventions();
        conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");
        conventions.DefiningEventsAs(type => type.Namespace == "Messages.Events");

        var endpointInstance = await Endpoint.Start(endpointConfiguration);

        Console.WriteLine("waiting for messages...");
        Console.ReadLine();

        await endpointInstance.Stop();
    }
}

