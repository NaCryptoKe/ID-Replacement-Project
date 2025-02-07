using ID_Replacement.Data.Models;
using Microsoft.Data.SqlClient;
using ID_Replacement.Data.Repositories.Interface;
using System;

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
                    var command = new SqlCommand("GetStudentById", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
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
                                Password = reader["Password"].ToString()
                            };
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
            try
            {
                using (var connection = DatabaseContext.Instance.GetConnection()) // Use singleton instance
                {
                    connection.Open();
                    var command = new SqlCommand("ValidateCredentials", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    return command.ExecuteScalar() != null;
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"Database Error: {sqlEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                return false;
            }
        }
    }
}
