using WorkTracker.Domain.Enums;

namespace WorkTracker.Domain.Models;

public class WorkDay
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public DayType Type { get; set; } = DayType.WorkDay;
    public string? Notes { get; set; }

    // Navigation
    public List<Stamp> Stamps { get; set; } = [];
    public List<LeaveSlot> LeaveSlots { get; set; } = [];

    // Computed
    public TimeSpan WorkedTime
    {
        get
        {
            var pairs = Stamps.OrderBy(s => s.Time).ToList();
            var total = TimeSpan.Zero;
            for (int i = 0; i + 1 < pairs.Count; i += 2)
                total += pairs[i + 1].Time - pairs[i].Time;
            return total;
        }
    }

    public TimeSpan LeaveTime =>
        TimeSpan.FromMinutes(LeaveSlots.Sum(l => l.Minutes));
}
