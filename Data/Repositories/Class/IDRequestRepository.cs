using System;
using System.Collections.Generic;
using ID_Replacement.Data;
using ID_Replacement.Data.Models;
using ID_Replacement.Data.Repositories.Interface;
using Microsoft.Data.SqlClient;

namespace ID_Replacement.Data.Repositories.Class
{
    public class IDRequestRepository : IIDRequestRepository
    {
        /// <summary>
        /// Retrieves an ID request by its ID.
        /// </summary>
        public IDRequest GetRequestById(int requestId)
        {
            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("GetRequestById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@RequestID", requestId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new IDRequest
                        {
                            RequestID = reader.GetInt32(0),
                            StudentID = reader.GetString(1),
                            RequestDate = reader.GetDateTime(2),
                            Status = reader.GetString(3),
                            NotificationSent = reader.GetBoolean(4)
                        };
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves all ID requests for a specific student.
        /// </summary>
        public IEnumerable<IDRequest> GetRequestsByStudentId(string studentId)
        {
            var requests = new List<IDRequest>();

            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("GetRequestsByStudentId", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@StudentID", studentId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        requests.Add(new IDRequest
                        {
                            RequestID = reader.GetInt32(0),
                            StudentID = reader.GetString(1),
                            RequestDate = reader.GetDateTime(2),
                            Status = reader.GetString(3),
                            NotificationSent = reader.GetBoolean(4)
                        });
                    }
                }
            }
            return requests;
        }

        /// <summary>
        /// Adds a new ID request to the database.
        /// </summary>
        public void AddRequest(IDRequest request)
        {
            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("AddRequest", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                // Ensure StudentID is valid
                if (string.IsNullOrWhiteSpace(request.StudentID))
                {
                    throw new ArgumentException("Invalid StudentID. Request must be linked to a valid student.");
                }

                command.Parameters.AddWithValue("@StudentID", request.StudentID);
                command.Parameters.AddWithValue("@Status", request.Status ?? "Pending");

                var requestIdParam = new SqlParameter("@RequestID", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                command.Parameters.Add(requestIdParam);

                command.ExecuteNonQuery();

                request.RequestID = (int)requestIdParam.Value;
            }
        }

        /// <summary>
        /// Updates the status of an ID request.
        /// </summary>
        public void UpdateRequestStatus(int requestId, string status)
        {
            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("UpdateRequestStatus", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@RequestID", requestId);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new Exception($"No request found with ID {requestId}");
                }
            }
        }
    }
}
