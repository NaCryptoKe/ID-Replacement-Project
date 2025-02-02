using System;
using System.Collections.Generic;
using ID_Replacement.Data;
using ID_Replacement.Data.Models;
using ID_Replacement.Data.Repositories.Interface;
using Microsoft.Data.SqlClient;

namespace ID_Replacement.Data.Repositories.Class
{
    public class AppointmentRepository : IAppointmentRepository
    {
        /// <summary>
        /// Retrieves an appointment by its ID.
        /// </summary>
        public Appointment GetAppointmentById(int appointmentId)
        {
            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var query = "SELECT AppointmentID, RequestID, AppointmentDate FROM Appointments WHERE AppointmentID = @AppointmentID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AppointmentID", appointmentId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Appointment
                            {
                                AppointmentID = reader.GetInt32(0),
                                RequestID = reader.GetInt32(1),
                                AppointmentDate = reader.GetDateTime(2)
                            };
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves all appointments for a specific request ID.
        /// </summary>
        public IEnumerable<Appointment> GetAppointmentsByRequestId(int requestId)
        {
            var appointments = new List<Appointment>();

            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var query = "SELECT AppointmentID, RequestID, AppointmentDate FROM Appointments WHERE RequestID = @RequestID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestID", requestId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appointments.Add(new Appointment
                            {
                                AppointmentID = reader.GetInt32(0),
                                RequestID = reader.GetInt32(1),
                                AppointmentDate = reader.GetDateTime(2)
                            });
                        }
                    }
                }
            }
            return appointments;
        }

        /// <summary>
        /// Adds a new appointment to the database.
        /// </summary>
        public void AddAppointment(Appointment appointment)
        {
            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();

                var query = @"INSERT INTO Appointments (RequestID, AppointmentDate) 
                              VALUES (@RequestID, @AppointmentDate);
                              SELECT SCOPE_IDENTITY();"; // Returns the new AppointmentID

                using (var command = new SqlCommand(query, connection))
                {
                    // Ensure RequestID exists to prevent foreign key errors
                    if (appointment.RequestID <= 0)
                    {
                        throw new ArgumentException("Invalid RequestID. Appointment must be linked to a valid request.");
                    }

                    command.Parameters.AddWithValue("@RequestID", appointment.RequestID);
                    command.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        appointment.AppointmentID = Convert.ToInt32(result);
                    }
                    else
                    {
                        throw new Exception("Failed to insert appointment.");
                    }
                }
            }
        }

        /// <summary>
        /// Updates the appointment date.
        /// </summary>
        public void UpdateAppointmentDate(int appointmentId, DateTime newDate)
        {
            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var query = "UPDATE Appointments SET AppointmentDate = @NewDate WHERE AppointmentID = @AppointmentID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NewDate", newDate);
                    command.Parameters.AddWithValue("@AppointmentID", appointmentId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new Exception($"No appointment found with ID {appointmentId}");
                    }
                }
            }
        }
    }
}
