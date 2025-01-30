using ID_Replacement.Data.Models;
using Microsoft.Data.SqlClient;
using ID_Replacement.Data.Repositories.Interface;

namespace ID_Replacement.Data.Repositories.Class
{
    public class DocumentRepository : IDocumentRepository
    {
        public Document GetDocumentById(int documentId)
        {
            // Implementation to fetch document by ID.
            return null;
        }

        public IEnumerable<Document> GetDocumentsByRequestId(int requestId)
        {
            // Implementation to fetch documents by request ID.
            return null;
        }

        public void AddDocument(Document document)
        {
            // Implementation to add a new document.
        }
    }
}
