using System;
using System.Collections.Generic;
using ID_Replacement.Data;
using ID_Replacement.Data.Models;
using ID_Replacement.Data.Repositories.Interface;
using Microsoft.Data.SqlClient;

namespace ID_Replacement.Data.Repositories.Class
{
    public class TransactionLogRepository : ITransactionLogRepository
    {
        /// <summary>
        /// Retrieves all logs related to a specific table.
        /// </summary>
        public IEnumerable<TransactionLog> GetLogs()
        {
            var logs = new List<TransactionLog>();

            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var query = "SELECT LogID, TableName, Operation, ChangeDate, Details, UserID FROM TransactionLogs";

                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(new TransactionLog
                            {
                                LogID = reader.GetInt32(0),
                                TableName = reader.GetString(1),
                                Operation = reader.GetString(2),
                                ChangeDate = reader.GetDateTime(3),
                                Details = reader.IsDBNull(4) ? null : reader.GetString(4),
                                UserID = reader.IsDBNull(5) ? null : reader.GetString(5)
                            });
                        }
                    }
                }
            }
            return logs;
        }
    }
}
