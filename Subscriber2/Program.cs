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
        BusConfiguration busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Samples.PubSub.Subscriber2");
        busConfiguration.UseSerialization<JsonSerializer>();
        busConfiguration.AuditProcessedMessagesTo("audit");
        busConfiguration.DisableFeature<AutoSubscribe>();
        busConfiguration.DisableFeature<SecondLevelRetries>();
        busConfiguration.UsePersistence<InMemoryPersistence>();
        busConfiguration.SendFailedMessagesTo("error");
        busConfiguration.EnableInstallers();
        busConfiguration.UseTransport<MsmqTransport>().Transactions(TransportTransactionMode.None);

        IEndpointInstance endpoint = await Endpoint.Start(busConfiguration);
        try
        {
            IBusSession busContext = endpoint.CreateBusSession();
            await busContext.Subscribe<IMyEvent>();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            await busContext.Unsubscribe<IMyEvent>();
        }
        finally
        {
            await endpoint.Stop();
        }
    }
}