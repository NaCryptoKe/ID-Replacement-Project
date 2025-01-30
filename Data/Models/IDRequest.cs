namespace ID_Replacement.Data.Models
{
    public class IDRequest
    {
        public int RequestID { get; set; }
        public string StudentID { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
        public bool NotificationSent { get; set; }
    }
}