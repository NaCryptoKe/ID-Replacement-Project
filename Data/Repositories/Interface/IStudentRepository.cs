using ID_Replacement.Data.Models;

namespace ID_Replacement.Data.Repositories.Interface
{
    public interface IStudentRepository
    {
        Student GetStudentById(string studentId);
        bool ValidateCredentials(string username, string password);

    }
}