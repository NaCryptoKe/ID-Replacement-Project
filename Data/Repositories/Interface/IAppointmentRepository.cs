using ID_Replacement.Data.Models;

namespace ID_Replacement.Data.Repositories.Interface
{
    public interface IAppointmentRepository
    {
        Appointment GetAppointmentById(int appointmentId);
        IEnumerable<Appointment> GetAppointmentsByRequestId(int requestId);
        void AddAppointment(Appointment appointment);
        void UpdateAppointmentDate(int appointmentId, DateTime newDate);
    }
}

