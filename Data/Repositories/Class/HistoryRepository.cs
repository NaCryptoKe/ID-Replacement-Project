using ID_Replacement.Data.Models;
using ID_Replacement.Data.Repositories.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ID_Replacement.Data.Repositories.Class
{
    public class HistoryRepository : IHistoryRepository
    {
        public IEnumerable<History> GetHistoryByStudentID(string studentID)
        {
            var history = new List<History>();

            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var query = "EXEC GetRequestsWithAppointmentByStudentId @StudentID";  // Call the procedure

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentID", studentID); // Add the parameter

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            history.Add(new History
                            {
                                StudentID = reader.IsDBNull(reader.GetOrdinal("StudentID")) ? string.Empty : reader.GetValue(reader.GetOrdinal("StudentID")).ToString(),
                                RequestDate = reader.IsDBNull(reader.GetOrdinal("RequestDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("RequestDate")),
                                AppointmentDate = reader.IsDBNull(reader.GetOrdinal("AppointmentDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                                Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? string.Empty : reader.GetString(reader.GetOrdinal("Status"))
                            });
                        }
                    }
                }
            }

            return history;
        }
    }
}
