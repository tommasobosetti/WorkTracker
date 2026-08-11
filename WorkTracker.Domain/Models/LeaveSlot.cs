namespace WorkTracker.Domain.Models;

public class LeaveSlot
{
    public int Id { get; set; }
    public int WorkDayId { get; set; }
    public TimeOnly From { get; set; }
    public TimeOnly To { get; set; }
    public string? Reason { get; set; }

    // Navigation
    public WorkDay WorkDay { get; set; } = null!;

    // Computed
    public int Minutes => (int)(To - From).TotalMinutes;
}
