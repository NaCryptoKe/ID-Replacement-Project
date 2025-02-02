using ID_Replacement.Data.Repositories.Class;
using ID_Replacement.Data.Repositories.Interface;
using ID_Replacement.Services.Interface;
using ID_Replacement.Services;
using ID_Replacement.Services.Class;

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
            IAdminViewModelRepository adminViewModelRepository = new AdminViewModelRepository();

            IAdminViewModelService adminViewModelService = new AdminViewModelService(adminViewModelRepository);

            // Start the AdminView Form with dependencies
            Application.Run(new AdminForm());
        }
    }
}
