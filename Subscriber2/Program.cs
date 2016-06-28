using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Features;
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
        EndpointConfiguration endpointConfiguration = new EndpointConfiguration("Samples.PubSub.Subscriber2");
        endpointConfiguration.UseSerialization<JsonSerializer>();
        endpointConfiguration.AuditProcessedMessagesTo("audit");
        endpointConfiguration.DisableFeature<AutoSubscribe>();
        endpointConfiguration.DisableFeature<SecondLevelRetries>();
        endpointConfiguration.UsePersistence<InMemoryPersistence>();
        endpointConfiguration.SendFailedMessagesTo("error");
        endpointConfiguration.EnableInstallers();
        endpointConfiguration.PurgeOnStartup(true);
        endpointConfiguration.UseTransport<RabbitMQTransport>().ConnectionString("host=localhost").UnicastRouting();
        endpointConfiguration.ScaleOut().InstanceDiscriminator(DateTime.Now.Second.ToString());

        IEndpointInstance endpoint = await Endpoint.Start(endpointConfiguration);
        try
        {
            await endpoint.Subscribe<IMyEvent>();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            await endpoint.Unsubscribe<IMyEvent>();
        }
        finally
        {
            await endpoint.Stop();
        }
    }
}
