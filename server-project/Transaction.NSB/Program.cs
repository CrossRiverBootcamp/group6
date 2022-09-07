using Messages.Commands;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Logging;
using System.Data.SqlClient;
using Transaction.Services.Extension;
using Transaction.Services.Interfaces;
using Transaction.Services.Services;

public class Program
{
    static ILog log = LogManager.GetLogger<Program>();

    static async Task Main()
    {
        Console.Title = "TransactionNSB";

        var endpointConfiguration = new EndpointConfiguration("TransactionNsb");
        //from options?
        var databaseConnection = "Data Source=DESKTOP-411ES1J\\ADMIN;Initial Catalog=TransactionsNSB;Integrated Security=True";
        var rabbitMQConnection = @"host=localhost";
        ;

        var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
        containerSettings.ServiceCollection.AddScoped<ITransactionService,TransactionService>();
        containerSettings.ServiceCollection.AddServices(databaseConnection);

        #region ReceiverConfiguration

        endpointConfiguration.EnableInstallers();
        endpointConfiguration.EnableOutbox();

        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(rabbitMQConnection);
        transport.UseConventionalRoutingTopology(QueueType.Quorum);

        var routing = transport.Routing();
        //change to a veriable
       routing.RouteToEndpoint(typeof(UpdateBalance), destination: "CustomerAccount");


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
