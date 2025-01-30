using ID_Replacement.Data.Models;
using Microsoft.Data.SqlClient;
using ID_Replacement.Data.Repositories.Interface;

namespace ID_Replacement.Data.Repositories.Class
{
    public class IDRequestRepository : IIDRequestRepository
    {
        public IDRequest GetRequestById(int requestId)
        {
            // Implementation to fetch request by ID.
            return null;
        }

        public IEnumerable<IDRequest> GetRequestsByStudentId(string studentId)
        {
            // Implementation to fetch all requests by StudentID.
            return null;
        }

        public void AddRequest(IDRequest request)
        {
            // Implementation to add a new ID request.
        }

        public void UpdateRequestStatus(int requestId, string status)
        {
            // Implementation to update the status of a request.
        }
    }
}
