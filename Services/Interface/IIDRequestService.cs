using ID_Replacement.Data.Models;

namespace ID_Replacement.Services.Interface
{
    public interface IIDRequestService
    {
        IDRequest GetRequestById(int requestId);
        IEnumerable<IDRequest> GetRequestsByStudentId(string studentId);
        void CreateRequest(IDRequest request);
        void UpdateRequestStatus(int requestId, string status);
    }
}
