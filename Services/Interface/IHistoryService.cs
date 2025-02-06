using ID_Replacement.Data.Models;

namespace ID_Replacement.Service.Interface
{
    public interface IHistoryService
    {
        List<History> GetHistoryByStudentID(string studentID);
    }
}
