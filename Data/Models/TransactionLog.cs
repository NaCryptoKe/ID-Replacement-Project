namespace ID_Replacement.Data.Models
{
    public class TransactionLog
    {
        public int LogID { get; set; }
        public string TableName { get; set; }
        public string Operation { get; set; }
        public DateTime ChangeDate { get; set; }
        public string Details { get; set; }
        public string UserID { get; set; }
    }
}