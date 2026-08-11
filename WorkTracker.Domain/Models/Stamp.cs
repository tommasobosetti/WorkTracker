namespace WorkTracker.Domain.Models;

public class Stamp
{
    public int Id { get; set; }
    public int WorkDayId { get; set; }
    public TimeOnly Time { get; set; }

    // Navigation
    public WorkDay WorkDay { get; set; } = null!;
}
