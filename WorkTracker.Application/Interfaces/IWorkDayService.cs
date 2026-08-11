using WorkTracker.Application.DTOs;
using WorkTracker.Domain.Enums;
using WorkTracker.Domain.Models;

namespace WorkTracker.Application.Interfaces;

public interface IWorkDayService
{
    Task<WorkDay> GetOrCreateTodayAsync();
    Task<WorkDay?> GetByDateAsync(DateOnly date);
    Task<List<WorkDaySummaryDto>> GetMonthSummariesAsync(int year, int month);
    Task<MonthSummaryDto> GetMonthStatsAsync(int year, int month);

    Task StampNowAsync();
    Task AddStampAsync(DateOnly date, TimeOnly time);
    Task DeleteStampAsync(int stampId);

    Task SetDayTypeAsync(DateOnly date, DayType type, string? notes = null);
    Task AddLeaveSlotAsync(DateOnly date, TimeOnly from, TimeOnly to, string? reason = null);
    Task DeleteLeaveSlotAsync(int slotId);
}
