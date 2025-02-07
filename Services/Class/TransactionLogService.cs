using ID_Replacement.Data.Models;
using ID_Replacement.Data.Repositories.Interface;
using ID_Replacement.Services.Interface;

namespace ID_Replacement.Services
{
    public class TransactionLogService : ITransactionLogService
    {
        private readonly ITransactionLogRepository _repository;

        public TransactionLogService(ITransactionLogRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TransactionLog> GetLogs()
        {
            return _repository.GetLogs();
        }
    }
}
