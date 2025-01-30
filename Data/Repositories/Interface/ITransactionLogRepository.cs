using ID_Replacement.Data.Models;

namespace ID_Replacement.Data.Repositories.Interface
{
    public interface ITransactionLogRepository
    {
        TransactionLog GetLogById(int logId);
        IEnumerable<TransactionLog> GetLogsByTableName(string tableName);
        void AddLog(TransactionLog log);
    }
}
