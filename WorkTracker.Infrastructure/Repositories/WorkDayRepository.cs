using Microsoft.EntityFrameworkCore;
using WorkTracker.Domain.Models;
using WorkTracker.Infrastructure.Data;

namespace WorkTracker.Infrastructure.Repositories;

public class WorkDayRepository(AppDbContext db)
{
    public async Task<WorkDay?> GetByDateAsync(DateOnly date) =>
        await db.WorkDays
            .Include(w => w.Stamps)
            .Include(w => w.LeaveSlots)
            .FirstOrDefaultAsync(w => w.Date == date);

    public async Task<List<WorkDay>> GetByMonthAsync(int year, int month) =>
        await db.WorkDays
            .Include(w => w.Stamps)
            .Include(w => w.LeaveSlots)
            .Where(w => w.Date.Year == year && w.Date.Month == month)
            .OrderBy(w => w.Date)
            .ToListAsync();

    public async Task<WorkDay> GetOrCreateTodayAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var day = await GetByDateAsync(today);
        if (day is not null) return day;

        day = new WorkDay { Date = today };
        db.WorkDays.Add(day);
        await db.SaveChangesAsync();
        return day;
    }

    public async Task SaveAsync(WorkDay day)
    {
        if (day.Id == 0)
            db.WorkDays.Add(day);
        else
            db.WorkDays.Update(day);

        await db.SaveChangesAsync();
    }

    public async Task DeleteStampAsync(int stampId)
    {
        var stamp = await db.Stamps.FindAsync(stampId);
        if (stamp is not null)
        {
            db.Stamps.Remove(stamp);
            await db.SaveChangesAsync();
        }
    }

    public async Task DeleteLeaveSlotAsync(int slotId)
    {
        var slot = await db.LeaveSlots.FindAsync(slotId);
        if (slot is not null)
        {
            db.LeaveSlots.Remove(slot);
            await db.SaveChangesAsync();
        }
    }
}
