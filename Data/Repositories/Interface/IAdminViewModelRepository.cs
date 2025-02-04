using ID_Replacement.Data.Models;

namespace ID_Replacement.Data.Repositories.Interface
{
    public interface IAdminViewModelRepository
    {
        IEnumerable<AdminViewModel> GetAllPendingStudents();
        IEnumerable<AdminViewModel> GetAllCompletedStudents();
        public bool AcceptRequest(AdminViewModel student);
        public bool DenyRequest(AdminViewModel student);
    }
}
