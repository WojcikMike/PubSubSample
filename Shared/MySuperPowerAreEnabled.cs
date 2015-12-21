using System;

public class MySuperPowerAreEnabled : IMyEvent
{
    public Guid EventId { get; set; }
    public DateTime? Time { get; set; }
    public TimeSpan Duration { get; set; }
}