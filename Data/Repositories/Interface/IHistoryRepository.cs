using ID_Replacement.Data.Models;
using System.Collections.Generic;

namespace ID_Replacement.Data.Repositories.Interface
{
    public interface IHistoryRepository
    {
        IEnumerable<History> GetHistoryByStudentID(string studentID);
    }
}
