using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ID_Replacement.Data.Models;
using ID_Replacement.Data.Repositories.Class;
using ID_Replacement.Data.Repositories.Interface;
using ID_Replacement.Service.Interface;
using ID_Replacement.Services.Interface;

namespace ID_Replacement.Services.Class
{
    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;

        public HistoryService(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }
        public List<History> GetHistoryByStudentID(string studentID)
        {
            var history = _historyRepository.GetHistoryByStudentID(studentID);
            return history?.ToList() ?? new List<History>();
        }
    }
}
