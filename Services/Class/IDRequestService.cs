using ID_Replacement.Data.Models;
using ID_Replacement.Data.Repositories.Interface;
using ID_Replacement.Services.Interface;

namespace ID_Replacement.Services
{
    public class IDRequestService : IIDRequestService
    {
        private readonly IIDRequestRepository _repository;

        public IDRequestService(IIDRequestRepository repository)
        {
            _repository = repository;
        }

        public IDRequest GetRequestById(int requestId)
        {
            return _repository.GetRequestById(requestId);
        }

        public IEnumerable<IDRequest> GetRequestsByStudentId(string studentId)
        {
            return _repository.GetRequestsByStudentId(studentId);
        }

        public void CreateRequest(IDRequest request)
        {
            _repository.AddRequest(request);
        }

        public void UpdateRequestStatus(int requestId, string status)
        {
            _repository.UpdateRequestStatus(requestId, status);
        }
    }
}
