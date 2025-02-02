using ID_Replacement.Data.Models;

namespace ID_Replacement.Services.Interface
{
    public interface IAdminViewModelService
    {
        IEnumerable<AdminViewModel> GetAllStudents();
    }
}