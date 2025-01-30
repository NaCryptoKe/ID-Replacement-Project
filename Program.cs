using System;
using System.Windows.Forms;
using ID_Replacement.Data.Repositories;
using ID_Replacement.Data.Repositories.Class;
using ID_Replacement.Data.Repositories.Interface;
using ID_Replacement.Services;
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

            // Initialize repositories and services
            IStudentRepository studentRepo = new StudentRepository();
            IStudentService studentService = new StudentService(studentRepo);

            // Pass the service to LoginForm
            Application.Run(new LoginForm(studentService));
        }
    }
}
