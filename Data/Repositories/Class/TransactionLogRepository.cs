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
        /// Retrieves a transaction log by its ID.
        /// </summary>
        public TransactionLog GetLogById(int logId)
        {
            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var query = "SELECT LogID, TableName, Operation, ChangeDate, Details, UserID FROM TransactionLogs WHERE LogID = @LogID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LogID", logId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TransactionLog
                            {
                                LogID = reader.GetInt32(0),
                                TableName = reader.GetString(1),
                                Operation = reader.GetString(2),
                                ChangeDate = reader.GetDateTime(3),
                                Details = reader.IsDBNull(4) ? null : reader.GetString(4),
                                UserID = reader.IsDBNull(5) ? null : reader.GetString(5)
                            };
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves all logs related to a specific table.
        /// </summary>
        public IEnumerable<TransactionLog> GetLogsByTableName(string tableName)
        {
            var logs = new List<TransactionLog>();

            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var query = "SELECT LogID, TableName, Operation, ChangeDate, Details, UserID FROM TransactionLogs WHERE TableName = @TableName";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TableName", tableName);

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

        /// <summary>
        /// Adds a new transaction log entry.
        /// </summary>
        public void AddLog(TransactionLog log)
        {
            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();

                var query = @"INSERT INTO TransactionLogs (TableName, Operation, Details, UserID) 
                              VALUES (@TableName, @Operation, @Details, @UserID);
                              SELECT SCOPE_IDENTITY();"; // Returns the new LogID

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TableName", log.TableName);
                    command.Parameters.AddWithValue("@Operation", log.Operation);
                    command.Parameters.AddWithValue("@Details", string.IsNullOrWhiteSpace(log.Details) ? DBNull.Value : (object)log.Details);
                    command.Parameters.AddWithValue("@UserID", string.IsNullOrWhiteSpace(log.UserID) ? DBNull.Value : (object)log.UserID);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        log.LogID = Convert.ToInt32(result);
                    }
                    else
                    {
                        throw new Exception("Failed to insert transaction log.");
                    }
                }
            }
        }
    }
}
