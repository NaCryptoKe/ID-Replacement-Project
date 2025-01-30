using ID_Replacement.Data.Models;

namespace ID_Replacement.Services.Interface
{
    public interface ITransactionLogService
    {
        TransactionLog GetLogById(int logId);
        IEnumerable<TransactionLog> GetLogsByTableName(string tableName);
        void LogTransaction(TransactionLog log);
    }
}
