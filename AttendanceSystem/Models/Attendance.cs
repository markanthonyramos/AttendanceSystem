using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AttendanceSystem.Models;

public class Attendance
{
    [PrimaryKey, AutoIncrement, Required]
    public int AttendanceId { get; set; }
    [Required, DataType(DataType.Text)]
    public string AttendanceTitle { get; set; }
    [ManyToOne]
    public List<Attendee> Attendees { get; set; }
    [Required, DataType(DataType.Date)]
    public DateTime DateFrom { get; set; }
    [Required, DataType(DataType.Date)]
    public DateTime DateTo { get; set; }
}