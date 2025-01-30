using System;
using System.Collections.Generic;
using System.Linq;

namespace ID_Replacement
{
    public class Admin
    {
        public class Request
        {
            public string FullName { get; set; }
            public string UserId { get; set; }
            public DateTime ScheduledDate { get; set; }
            public string Remarks { get; set; }
            public string FilePath { get; set; }
            public string Facts { get; set; }
            public RequestStatus Status { get; set; }
        }

        public enum RequestStatus
        {
            Pending,
            Accepted,
            Denied
        }

        private List<Request> _requests;

        public Admin()
        {
            _requests = new List<Request>();
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            _requests.Add(new Request
            {
                FullName = "John Doe",
                UserId = "JD123",
                Facts = "Senior Developer, 5 years experience",
                Status = RequestStatus.Pending,
                ScheduledDate = DateTime.Now.AddDays(5),
                Remarks = "Urgent review needed",
                FilePath = @"C:\Documents\JohnDoe_Resume.pdf"
            });

            _requests.Add(new Request
            {
                FullName = "Jane Smith",
                UserId = "JS456",
                Facts = "Project Manager, Certified PMP",
                Status = RequestStatus.Pending,
                ScheduledDate = DateTime.Now.AddDays(3),
                Remarks = "Priority candidate",
                FilePath = @"C:\Documents\JaneSmith_Portfolio.pdf"
            });
        }

        public List<Request> GetPendingRequests()
        {
            return _requests.Where(r => r.Status == RequestStatus.Pending).ToList();
        }

        public List<Request> GetCompletedRequests()
        {
            return _requests.Where(r => r.Status != RequestStatus.Pending).ToList();
        }

        public bool AcceptRequest(string userId)
        {
            var request = _requests.FirstOrDefault(r => r.UserId == userId);
            if (request != null && request.Status == RequestStatus.Pending)
            {
                request.Status = RequestStatus.Accepted;
                return true;
            }
            return false;
        }

        public bool DenyRequest(string userId)
        {
            var request = _requests.FirstOrDefault(r => r.UserId == userId);
            if (request != null && request.Status == RequestStatus.Pending)
            {
                request.Status = RequestStatus.Denied;
                return true;
            }
            return false;
        }

        public Request GetRequestDetails(string userId)
        {
            return _requests.FirstOrDefault(r => r.UserId == userId);
        }

        public void AddRequest(string fullName, string userId, string facts, DateTime scheduledDate, string remarks, string filePath)
        {
            _requests.Add(new Request
            {
                FullName = fullName,
                UserId = userId,
                Facts = facts,
                Status = RequestStatus.Pending,
                ScheduledDate = scheduledDate,
                Remarks = remarks,
                FilePath = filePath
            });
        }

        public bool DeleteRequest(string userId)
        {
            var request = _requests.FirstOrDefault(r => r.UserId == userId);
            if (request != null && request.Status != RequestStatus.Pending)
            {
                _requests.Remove(request);
                return true;
            }
            return false;
        }
    }
}
