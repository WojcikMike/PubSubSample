using System;
using System.Threading.Tasks;
using NServiceBus;

public class EventMessageHandler : IHandleMessages<IMyEvent>
{
    public Task Handle(IMyEvent message, IMessageHandlerContext context)
    {
        Console.WriteLine("Subscriber 2 received IEvent with Id {0}.", message.EventId);
        Console.WriteLine("Message time: {0}.", message.Time);
        Console.WriteLine("Message duration: {0}.", message.Duration);

        context.Publish(new MySuperPowerAreEnabled()
        {
            EventId = Guid.NewGuid(),
            Time = DateTime.Now,
            Duration = TimeSpan.FromSeconds(99999D)
        });

        context.SendLocal(new LocalCommand()
        {
            EventId = Guid.NewGuid(),
            Time = DateTime.Now,
            Duration = TimeSpan.FromSeconds(99999D)
        });

        throw new Exception("jada jada jada");
        //return Task.FromResult(0);
    }

}