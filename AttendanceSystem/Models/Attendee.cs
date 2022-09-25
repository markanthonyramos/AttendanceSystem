using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models;
public class Attendee
{
    [PrimaryKey, AutoIncrement, Required]
    public int AttendeeId { get; set; }
    [Required]
    public string AttendeeName { get; set; }
    [Required]
    public string AttendanceStatus { get; set; }
    public DateTime CheckInDateTime { get; set; }

    public int AttendanceId { get; set; }
    [OneToMany]
    public Attendance Attendance { get; set; }
}