using ID_Replacement.Data.Models;
using ID_Replacement.Data.Repositories.Interface;
using ID_Replacement.Services.Interface;

namespace ID_Replacement.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentService(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public Appointment GetAppointmentById(int appointmentId)
        {
            return _repository.GetAppointmentById(appointmentId);
        }

        public IEnumerable<Appointment> GetAppointmentsByRequestId(int requestId)
        {
            return _repository.GetAppointmentsByRequestId(requestId);
        }

        public void ScheduleAppointment(Appointment appointment)
        {
            _repository.AddAppointment(appointment);
        }

        public void RescheduleAppointment(int appointmentId, DateTime newDate)
        {
            _repository.UpdateAppointmentDate(appointmentId, newDate);
        }
    }
}
