using Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;

public class Program
{
    static ILog log = LogManager.GetLogger<Program>();
    static async Task RunLoop(IEndpointInstance endpointInstance)
    {
        while (true)
        {
            await endpointInstance.ScheduleEvery(
         timeSpan: TimeSpan.FromHours(2),
         task: pipelineContext =>
         {
             var message = new DeleteExpiredCodes();
             return pipelineContext.Send(message);
         });
        }

    }

    static async Task Main()
    {
        Console.Title = "Scheduling";

        var endpointConfiguration = new EndpointConfiguration("Scheduling");
        endpointConfiguration.SendOnly();
        var rabbitMQConnection = @"host=localhost";

        var transport =  endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(rabbitMQConnection);
        transport.UseConventionalRoutingTopology(QueueType.Quorum);

        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(DeleteExpiredCodes), "CustomerAccount.WebAPI");


        var conventions = endpointConfiguration.Conventions();
        conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");

        var endpointInstance = await Endpoint.Start(endpointConfiguration);

         await RunLoop(endpointInstance);

        await endpointInstance.Stop();
    }
}
