using ID_Replacement.Data.Repositories.Class;
using ID_Replacement.Data.Repositories.Interface;
using ID_Replacement.Services.Interface;
using ID_Replacement.Services;
using ID_Replacement.Services.Class;
using ID_Replacement.Data.Models;

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
            IHistoryRepository historyRepository = new HistoryRepository();

            IAdminViewModelService adminViewModelService = new AdminViewModelService(adminViewModelRepository);
            IStudentService studentService = new StudentService(studentRepository);

            Student student = new Student();
            student.StudentID = "1";

            // Start the AdminView Form with dependencies
            //Application.Run(new AdminForm(adminViewModelRepository));
            Application.Run(new LoginForm(studentService));
        }
    }
}
