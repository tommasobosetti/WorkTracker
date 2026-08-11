using WorkTracker.Application.DTOs;
using WorkTracker.Application.Interfaces;
using WorkTracker.Domain.Enums;
using WorkTracker.Domain.Models;
using WorkTracker.Infrastructure.Repositories;

namespace WorkTracker.Application.Services;

public class WorkDayService(WorkDayRepository repo) : IWorkDayService
{
    public Task<WorkDay> GetOrCreateTodayAsync() =>
        repo.GetOrCreateTodayAsync();

    public Task<WorkDay?> GetByDateAsync(DateOnly date) =>
        repo.GetByDateAsync(date);

    public async Task<List<WorkDaySummaryDto>> GetMonthSummariesAsync(int year, int month)
    {
        var days = await repo.GetByMonthAsync(year, month);
        return days.Select(ToSummary).ToList();
    }

    public async Task<MonthSummaryDto> GetMonthStatsAsync(int year, int month)
    {
        var days = await repo.GetByMonthAsync(year, month);
        return new MonthSummaryDto(
            year, month,
            days.Count(d => d.Type == DayType.WorkDay),
            days.Count(d => d.Type == DayType.SmartWorking),
            days.Count(d => d.Type == DayType.Vacation),
            days.Count(d => d.Type == DayType.Illness),
            TimeSpan.FromTicks(days.Sum(d => d.WorkedTime.Ticks)),
            TimeSpan.FromTicks(days.Sum(d => d.LeaveTime.Ticks))
        );
    }

    public async Task StampNowAsync()
    {
        var day = await repo.GetOrCreateTodayAsync();
        day.Stamps.Add(new Stamp { Time = TimeOnly.FromDateTime(DateTime.Now) });
        await repo.SaveAsync(day);
    }

    public async Task AddStampAsync(DateOnly date, TimeOnly time)
    {
        var day = await repo.GetByDateAsync(date) ?? new WorkDay { Date = date };
        day.Stamps.Add(new Stamp { Time = time });
        await repo.SaveAsync(day);
    }

    public Task DeleteStampAsync(int stampId) =>
        repo.DeleteStampAsync(stampId);

    public async Task SetDayTypeAsync(DateOnly date, DayType type, string? notes = null)
    {
        var day = await repo.GetByDateAsync(date) ?? new WorkDay { Date = date };
        day.Type = type;
        if (notes is not null) day.Notes = notes;
        await repo.SaveAsync(day);
    }

    public async Task AddLeaveSlotAsync(DateOnly date, TimeOnly from, TimeOnly to, string? reason = null)
    {
        var day = await repo.GetByDateAsync(date) ?? new WorkDay { Date = date };
        day.LeaveSlots.Add(new LeaveSlot { From = from, To = to, Reason = reason });
        await repo.SaveAsync(day);
    }

    public Task DeleteLeaveSlotAsync(int slotId) =>
        repo.DeleteLeaveSlotAsync(slotId);

    // ── private helpers ──────────────────────────────────────────────────────

    private static WorkDaySummaryDto ToSummary(WorkDay d) =>
        new(d.Id, d.Date, d.Type, d.WorkedTime, d.LeaveTime, d.Stamps.Count, d.Notes);
}
