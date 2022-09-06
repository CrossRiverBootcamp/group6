using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Logging;
using System.Data.SqlClient;

public class Program
{
    static ILog log = LogManager.GetLogger<Program>();

    static async Task Main()
    {
        Console.Title = "TransactionNSB";

        var endpointConfiguration = new EndpointConfiguration("TransactionNsb");

        var databaseConnection = "Data Source=localhost\\sqlexpress;Initial Catalog=Transactions;Integrated Security=True";
        var rabbitMQConnection = @"host=localhost";

        var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
/*        containerSettings.ServiceCollection.AddScoped<ISubscriberService, SubscriberService>();
        containerSettings.ServiceCollection.AddScoped<ISubscriberDAL, SubscriberDAL>();
        containerSettings.ServiceCollection.AddDbContextFactory<SubscriberContext>(opt => opt.UseSqlServer(databaseConnection));*/

        #region ReceiverConfiguration

        endpointConfiguration.EnableInstallers();
        endpointConfiguration.EnableOutbox();

        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(rabbitMQConnection);
        transport.UseConventionalRoutingTopology(QueueType.Quorum);

/*        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(UpdateTracking), destination: "Tracking");
        routing.RouteToEndpoint(typeof(UpdateStatus), destination: "Measure");*/

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
        #endregion

        var conventions = endpointConfiguration.Conventions();
        conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");
        conventions.DefiningEventsAs(type => type.Namespace == "Messages.Events");

        var endpointInstance = await Endpoint.Start(endpointConfiguration);

        Console.WriteLine("waiting for messages...");
        Console.ReadLine();

        await endpointInstance.Stop();
    }
}
