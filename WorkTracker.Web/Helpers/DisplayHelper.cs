using WorkTracker.Domain.Enums;

namespace WorkTracker.Web.Helpers;

public static class DisplayHelper
{
    public static string DayTypeLabel(DayType t) => t switch
    {
        DayType.WorkDay      => "Ufficio",
        DayType.SmartWorking => "Smart Working",
        DayType.Vacation     => "Ferie",
        DayType.Illness      => "Malattia",
        DayType.Holiday      => "Festività",
        _                    => t.ToString()
    };

    public static string DayTypeBg(DayType t) => t switch
    {
        DayType.WorkDay      => "#DBEAFE",
        DayType.SmartWorking => "#EDE9FE",
        DayType.Vacation     => "#DCFCE7",
        DayType.Illness      => "#FEF9C3",
        DayType.Holiday      => "#E0F2FE",
        _                    => "transparent"
    };

    public static string FormatWorkedTime(TimeSpan t) =>
        $"{(int)t.TotalHours:D2}:{t.Minutes:D2}";

    public static string FormatWorkedTimeLong(TimeSpan t) =>
        $"{(int)t.TotalHours}h {t.Minutes:D2}m";
}
