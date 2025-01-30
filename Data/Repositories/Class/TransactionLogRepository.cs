using ID_Replacement.Data.Models;
using Microsoft.Data.SqlClient;
using ID_Replacement.Data.Repositories.Interface;

namespace ID_Replacement.Data.Repositories.Class
{
    public class TransactionLogRepository : ITransactionLogRepository
    {
        public TransactionLog GetLogById(int logId)
        {
            // Implementation to fetch a log by ID.
            return null;
        }

        public IEnumerable<TransactionLog> GetLogsByTableName(string tableName)
        {
            // Implementation to fetch logs by table name.
            return null;
        }

        public void AddLog(TransactionLog log)
        {
            // Implementation to add a new transaction log.
        }
    }
}
