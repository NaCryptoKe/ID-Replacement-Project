using System;
using System.Windows.Forms;
using ID_Replacement.Data.Repositories.Class;
using ID_Replacement.Data.Repositories.Interface;
using ID_Replacement.Services;
using ID_Replacement.Services.Class;
using ID_Replacement.Services.Interface;

namespace ID_Replacement
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize necessary repositories
            IStudentRepository studentRepository = new StudentRepository();
            IAdminViewModelRepository adminViewModelRepository = new AdminViewModelRepository();

            // Initialize services
            IStudentService studentService = new StudentService(studentRepository);
            IAdminViewModelService adminViewModelService = new AdminViewModelService(adminViewModelRepository);

            // Start with Login Form
            //Application.Run(new LoginForm(studentService));
            Application.Run(new AdminForm(adminViewModelRepository));
        }
    }
}
