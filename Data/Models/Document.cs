namespace ID_Replacement.Data.Models
{
    public class Document
    {
        public int DocumentID { get; set; }
        public int RequestID { get; set; }
        public string DocumentPath { get; set; }
        public DateTime UploadDate { get; set; }
    }
}