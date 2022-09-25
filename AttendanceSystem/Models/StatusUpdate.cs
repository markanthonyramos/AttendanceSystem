namespace AttendanceSystem.Models
{
    public class StatusUpdate
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public StatusUpdate(bool status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
