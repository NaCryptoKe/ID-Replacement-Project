using ID_Replacement.Data.Models;
using Microsoft.Data.SqlClient;
using ID_Replacement.Data.Repositories.Interface;

namespace ID_Replacement.Data.Repositories.Class
{
    public class StudentRepository : IStudentRepository
    {
        public Student GetStudentById(string identifier)
        {
            try
            {
                using (var connection = DatabaseContext.Instance.GetConnection()) // Use singleton instance
                {
                    connection.Open();
                    var query = "SELECT StudentID, FullName, Email, Department, Year, Password FROM Students WHERE StudentID = @Identifier OR Email = @Identifier";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Identifier", identifier);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Student
                                {
                                    StudentID = reader["StudentID"].ToString(),
                                    FullName = reader["FullName"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Department = reader["Department"].ToString(),
                                    Year = Convert.ToInt32(reader["Year"]),
                                    Password = reader["Password"].ToString() // TODO: Remove this when using hashed passwords
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Database Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
            }

            return null; // Return null if no record is found or an error occurs
        }


        public bool ValidateCredentials(string username, string password)
        {
            using (var connection = DatabaseContext.Instance.GetConnection()) // Use singleton instance
            {
                connection.Open();
                var query = @"SELECT StudentID FROM Students 
                              WHERE (Email = @username OR StudentID = @username) 
                              AND Password = @password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);
                    return command.ExecuteScalar() != null;
                }
            }
        }
    }
}
