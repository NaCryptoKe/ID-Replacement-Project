using ID_Replacement.Data.Models;

namespace ID_Replacement.Services.Interface
{
    public interface IStudentService
    {
        Student GetStudentById(string studentId);
        bool ValidateCredentials(string username, string password);
    }
}