using ID_Replacement.Data.Repositories.Interface;
using ID_Replacement.Data.Models;
using ID_Replacement.Services.Interface;
using ID_Replacement.Data.Repositories.Class;

namespace ID_Replacement.Services.Class
{
    public class AdminViewModelService : IAdminViewModelService
    {
        private readonly IAdminViewModelRepository _adminViewModelRepository;
        public AdminViewModelService(IAdminViewModelRepository adminViewModelRepository)
        {
            _adminViewModelRepository = adminViewModelRepository;
        }
        public List<AdminViewModel> GetAllPendingStudents()
        {
            return _adminViewModelRepository.GetAllPendingStudents().ToList();
        }
        public List<AdminViewModel> GetAllCompletedStudents()
        {
            return _adminViewModelRepository.GetAllCompletedStudents().ToList();
        }


    }
}
