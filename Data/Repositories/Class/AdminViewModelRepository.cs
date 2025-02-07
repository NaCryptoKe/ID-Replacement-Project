using ID_Replacement.Data.Models;
using Microsoft.Data.SqlClient;
using ID_Replacement.Data.Repositories.Interface;
using System;
using System.Collections.Generic;

namespace ID_Replacement.Data.Repositories.Class
{
    public class AdminViewModelRepository : IAdminViewModelRepository
    {
        public IEnumerable<AdminViewModel> GetAllPendingStudents()
        {
            var adminViewModel = new List<AdminViewModel>();
            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var query = "EXEC GetAllPendingStudents"; // Call the stored procedure

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        adminViewModel.Add(new AdminViewModel
                        {
                            RequestID = reader.IsDBNull(reader.GetOrdinal("RequestID")) ? string.Empty : reader.GetValue(reader.GetOrdinal("RequestID")).ToString(),
                            StudentID = reader.IsDBNull(reader.GetOrdinal("StudentID")) ? string.Empty : reader.GetValue(reader.GetOrdinal("StudentID")).ToString(),
                            FullName = reader.IsDBNull(reader.GetOrdinal("FullName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FullName")),
                            AppointmentDate = reader.IsDBNull(reader.GetOrdinal("AppointmentDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                            Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? string.Empty : reader.GetString(reader.GetOrdinal("Status")),
                            FilePath = reader.IsDBNull(reader.GetOrdinal("DocumentPath")) ? string.Empty : reader.GetString(reader.GetOrdinal("DocumentPath")),
                            Remarks = reader.IsDBNull(reader.GetOrdinal("Remark")) ? string.Empty : reader.GetString(reader.GetOrdinal("Remark"))
                        });
                    }
                }
            }
            return adminViewModel;
        }

        public IEnumerable<AdminViewModel> GetAllCompletedStudents()
        {
            var adminViewModel = new List<AdminViewModel>();
            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var query = "EXEC GetAllCompletedStudents"; // Call the stored procedure

                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        adminViewModel.Add(new AdminViewModel
                        {
                            StudentID = reader.IsDBNull(reader.GetOrdinal("StudentID")) ? string.Empty : reader.GetString(reader.GetOrdinal("StudentID")),
                            FullName = reader.IsDBNull(reader.GetOrdinal("FullName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FullName")),
                            AppointmentDate = reader.IsDBNull(reader.GetOrdinal("AppointmentDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                            Status = reader.IsDBNull(reader.GetOrdinal("Status")) ? string.Empty : reader.GetString(reader.GetOrdinal("Status")),
                            FilePath = reader.IsDBNull(reader.GetOrdinal("DocumentPath")) ? string.Empty : reader.GetString(reader.GetOrdinal("DocumentPath")),
                            Remarks = reader.IsDBNull(reader.GetOrdinal("Remark")) ? string.Empty : reader.GetString(reader.GetOrdinal("Remark"))
                        });
                    }
                }
            }
            return adminViewModel;
        }

        public bool AcceptRequest(AdminViewModel student)
        {
            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var query = "EXEC AcceptRequest @RequestID"; // Call the stored procedure

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestID", student.RequestID);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Returns true if at least one row was updated
                }
            }
        }

        public bool DenyRequest(AdminViewModel student)
        {
            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var query = "EXEC DenyRequest @RequestID"; // Call the stored procedure

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestID", student.RequestID);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0; // Returns true if at least one row was updated
                }
            }
        }
    }
}
