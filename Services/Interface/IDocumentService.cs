using ID_Replacement.Data.Models;

namespace ID_Replacement.Services.Interface
{
    public interface IDocumentService
    {
        Document GetDocumentById(int documentId);
        IEnumerable<Document> GetDocumentsByRequestId(int requestId);
        void CreateDocument(Document document);
    }
}
