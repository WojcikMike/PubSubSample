
using System;

public class LocalCommand : NServiceBus.ICommand
{
    public Guid EventId { get; set; }
    public DateTime? Time { get; set; }
    public TimeSpan Duration { get; set; }
}