using System;
using NServiceBus;

public class MySuperPowerAreEnabled : IEvent
{
    public Guid EventId { get; set; }
    public DateTime? Time { get; set; }
    public TimeSpan Duration { get; set; }
}