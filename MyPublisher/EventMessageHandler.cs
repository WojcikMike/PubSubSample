using System;
using System.Threading.Tasks;
using NServiceBus;

public class EventMessageHandler : IHandleMessages<MySuperPowerAreEnabled>
{
    public Task Handle(MySuperPowerAreEnabled message, IMessageHandlerContext context)
    {
        Console.WriteLine("Publisher received MySuperPowerAreEnabled with Id {0}.", message.EventId);
        Console.WriteLine("Message time: {0}.", message.Time);
        Console.WriteLine("Message duration: {0}.", message.Duration);
        
        return Task.FromResult(0);
    }

}