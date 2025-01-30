using ID_Replacement.Data.Models;
using ID_Replacement.Data.Repositories.Interface;
using ID_Replacement.Services.Interface;

namespace ID_Replacement.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repository;

        public DocumentService(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public Document GetDocumentById(int documentId)
        {
            return _repository.GetDocumentById(documentId);
        }

        public IEnumerable<Document> GetDocumentsByRequestId(int requestId)
        {
            return _repository.GetDocumentsByRequestId(requestId);
        }

        public void CreateDocument(Document document)
        {
            _repository.AddDocument(document);
        }
    }
}
