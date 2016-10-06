using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

static class Program
{
    static void Main()
    {
        AsyncMain().GetAwaiter().GetResult();
    }

    static async Task AsyncMain()
    {
        LogManager.Use<DefaultFactory>().Level(LogLevel.Info);
        EndpointConfiguration endpointConfiguration = new EndpointConfiguration("Samples.PubSub.Subscriber1");
        endpointConfiguration.AuditProcessedMessagesTo("audit");
        endpointConfiguration.UseSerialization<JsonSerializer>();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.PurgeOnStartup(true);
        
        endpointConfiguration.UseTransport<RabbitMQTransport>().ConnectionString("host=localhost");

        IEndpointInstance endpoint = await Endpoint.Start(endpointConfiguration);
        try
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        finally
        {
            await endpoint.Stop();
        }
    }

}