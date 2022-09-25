using AttendanceSystem.Models;
using SQLite;

namespace AttendanceSystem.Data;
public class IAttendanceService
{
    string _dbPath;
    public string StatusMessage { get; set; }
    private static SQLiteAsyncConnection conn;

    public IAttendanceService(string dbPath)
    {
        _dbPath = dbPath;
    }
    private async Task InitAsync()
    {
        // Don't Create database if it exists
        if (conn != null)
            return;
        // Create database and Attendance Table
        conn = new SQLiteAsyncConnection(_dbPath);
        await conn.CreateTableAsync<Attendance>();
        await conn.CreateTableAsync<Attendee>();
    }
    public async Task<List<Attendance>> GetAllAttendanceAsync()
    {
        await InitAsync();

        var attendees = await conn.Table<Attendee>().ToListAsync();

        var attendances = await conn.Table<Attendance>().ToListAsync();

        return attendances.Select(attendance => new Attendance
        {
            AttendanceId = attendance.AttendanceId,
            AttendanceTitle = attendance.AttendanceTitle,
            Attendees = attendees.Where(attendee => attendee.AttendanceId == attendance.AttendanceId).ToList(),
            DateFrom = attendance.DateFrom,
            DateTo = attendance.DateTo
        }).ToList();
    }
    public async Task<Attendance> GetAttendanceAsync(int id)
    {
        var attendees = await conn.Table<Attendee>().Where(a => a.AttendanceId == id).ToListAsync();

        var attendance = await conn.Table<Attendance>().Where(a => a.AttendanceId == id).ToListAsync();

        var newAttendance = new Attendance();

        attendance.ForEach(a =>
        {
            newAttendance.AttendanceId = a.AttendanceId;
            newAttendance.AttendanceTitle = a.AttendanceTitle;
            newAttendance.Attendees = attendees;
            newAttendance.DateFrom = a.DateFrom;
            newAttendance.DateTo = a.DateTo;
        });

        return newAttendance;
    }
    public async Task<StatusUpdate> CreateAttendanceAsync(Attendance paramAttendance)
    {
        try
        {
            await conn.InsertAsync(paramAttendance);
        } catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new StatusUpdate(false, "An error occured.");
        }

        return new StatusUpdate(true, "Attendance has been created.");
    }
    public async Task<StatusUpdate> UpdateAttendanceAsync(Attendance paramAttendance)
    {
        try
        {
            await conn.UpdateAsync(paramAttendance);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new StatusUpdate(false, "An error occured.");
        }

        return new StatusUpdate(true, "Attendance has been updated.");
    }
    public async Task<StatusUpdate> AddAttendeeAsync(Attendee paramAttendee)
    {
        try
        {
            await conn.InsertAsync(paramAttendee);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new StatusUpdate(false, "An error occured.");
        }

        return new StatusUpdate(true, "Attendance has been updated.");
    }
    public async Task<StatusUpdate> DeleteAttendanceAsync(Attendance paramAttendance)
    {
        try
        {
            await conn.DeleteAsync(paramAttendance);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new StatusUpdate(false, "An error occured.");
        }

        return new StatusUpdate(true, "Attendance has been deleted.");
    }
}