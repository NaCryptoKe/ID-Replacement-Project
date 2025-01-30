using ID_Replacement.Data.Models;

namespace ID_Replacement.Services.Interface
{
    public interface IAppointmentService
    {
        Appointment GetAppointmentById(int appointmentId);
        IEnumerable<Appointment> GetAppointmentsByRequestId(int requestId);
        void ScheduleAppointment(Appointment appointment);
        void RescheduleAppointment(int appointmentId, DateTime newDate);
    }
}
