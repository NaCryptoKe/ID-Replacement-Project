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

        public TransactionLog GetLogById(int logId)
        {
            return _repository.GetLogById(logId);
        }

        public IEnumerable<TransactionLog> GetLogsByTableName(string tableName)
        {
            return _repository.GetLogsByTableName(tableName);
        }

        public void LogTransaction(TransactionLog log)
        {
            _repository.AddLog(log);
        }
    }
}
