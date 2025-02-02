using ID_Replacement.Data.Models;
using Microsoft.Data.SqlClient;
using ID_Replacement.Data.Repositories.Interface;

namespace ID_Replacement.Data.Repositories.Class
{
    public class AdminViewModelRepository : IAdminViewModelRepository
    {
        public IEnumerable<AdminViewModel> GetAllStudents()
        {
            var adminViewModel = new List<AdminViewModel>();
            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var query = "SELECT * FROM StudentAppointments";
                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        adminViewModel.Add(new AdminViewModel
                        {
                            StudentID = reader.GetString(reader.GetOrdinal("StudentID")),
                            FullName = reader.GetString(reader.GetOrdinal("FullName")),
                            AppointmentDate = reader.GetDateTime(reader.GetOrdinal("AppointmentDate")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        });

                    }
                }
            }
            return adminViewModel; ;
        }
    }
}
