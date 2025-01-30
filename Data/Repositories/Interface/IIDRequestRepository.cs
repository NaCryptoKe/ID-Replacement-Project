using ID_Replacement.Data.Models;

namespace ID_Replacement.Data.Repositories.Interface
{
    public interface IIDRequestRepository
    {
        IDRequest GetRequestById(int requestId);
        IEnumerable<IDRequest> GetRequestsByStudentId(string studentId);
        void AddRequest(IDRequest request);
        void UpdateRequestStatus(int requestId, string status);
    }
}
