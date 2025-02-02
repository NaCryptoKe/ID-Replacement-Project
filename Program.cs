using ID_Replacement.Data.Repositories.Class;
using ID_Replacement.Data.Repositories.Interface;

namespace ID_Replacement
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize repositories
            IStudentRepository studentRepository = new StudentRepository();
            IIDRequestRepository idRequestRepository = new IDRequestRepository();
            IAppointmentRepository appointmentRepository = new AppointmentRepository();

            // Start the AdminView Form with dependencies
        }
    }
}
