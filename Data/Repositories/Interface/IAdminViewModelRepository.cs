using ID_Replacement.Data.Models;

namespace ID_Replacement.Data.Repositories.Interface
{
    public interface IAdminViewModelRepository
    {
        IEnumerable<AdminViewModel> GetAllStudents();
    }
}
