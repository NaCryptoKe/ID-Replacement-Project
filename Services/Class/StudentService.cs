using ID_Replacement.Data.Models;
using ID_Replacement.Data.Repositories.Interface;
using ID_Replacement.Services.Interface;

namespace ID_Replacement.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Student GetStudentById(string studentId)
        {
            return _studentRepository.GetStudentById(studentId);
        }

        public bool ValidateCredentials(string username, string password)
        {
            return _studentRepository.ValidateCredentials(username, password);
        }
    }
}