using ID_Replacement.Data.Models;

namespace ID_Replacement.Data.Repositories.Interface
{
    public interface IDocumentRepository
    {
        Document GetDocumentById(int documentId);
        IEnumerable<Document> GetDocumentsByRequestId(int requestId);
        void AddDocument(Document document);
    }
}
