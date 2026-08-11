using WorkTracker.Domain.Enums;

namespace WorkTracker.Application.DTOs;

public record WorkDaySummaryDto(
    int Id,
    DateOnly Date,
    DayType Type,
    TimeSpan WorkedTime,
    TimeSpan LeaveTime,
    int StampCount,
    string? Notes
);

public record MonthSummaryDto(
    int Year,
    int Month,
    int WorkDays,
    int SmartWorkingDays,
    int VacationDays,
    int IllnessDays,
    TimeSpan TotalWorkedTime,
    TimeSpan TotalLeaveTime
);
