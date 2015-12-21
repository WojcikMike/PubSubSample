using System;
using System.Threading.Tasks;
using NServiceBus;

public class LocalMessageHandler : IHandleMessages<LocalCommand>
{
    public Task Handle(LocalCommand message, IMessageHandlerContext context)
    {
        Console.WriteLine("Subscriber 2 received LocalCommand with Id {0}.", message.EventId);
        Console.WriteLine("Message time: {0}.", message.Time);
        Console.WriteLine("Message duration: {0}.", message.Duration);
        
        return Task.FromResult(0);
    }

}