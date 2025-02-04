using ID_Replacement.Data.Models;

namespace ID_Replacement.Services.Interface
{
    public interface IAdminViewModelService
    {
        List<AdminViewModel> GetAllPendingStudents();
        List<AdminViewModel> GetAllCompletedStudents();
    }
}