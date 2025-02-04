namespace ID_Replacement.Data.Models
{
    public class AdminViewModel
    {
        public string RequestID { get; set; }
        public string StudentID { get; set; }
        public string FullName { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string Status { get; set; }

        public string Remarks { get; set; }

        public string FilePath { get; set; }
        public RequestStatus Statuses { get; set; }

        public enum RequestStatus
        {
            Pending,
            Accepted,
            Denied
        }
    }
}
