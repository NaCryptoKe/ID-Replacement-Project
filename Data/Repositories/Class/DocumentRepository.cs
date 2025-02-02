using System;
using System.Collections.Generic;
using ID_Replacement.Data;
using ID_Replacement.Data.Models;
using ID_Replacement.Data.Repositories.Interface;
using Microsoft.Data.SqlClient;

namespace ID_Replacement.Data.Repositories.Class
{
    public class DocumentRepository : IDocumentRepository
    {
        /// <summary>
        /// Retrieves a document by its ID.
        /// </summary>
        public Document GetDocumentById(int documentId)
        {
            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var query = "SELECT DocumentID, RequestID, DocumentPath, UploadDate FROM Documents WHERE DocumentID = @DocumentID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DocumentID", documentId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Document
                            {
                                DocumentID = reader.GetInt32(0),
                                RequestID = reader.GetInt32(1),
                                DocumentPath = reader.GetString(2),
                                UploadDate = reader.GetDateTime(3)
                            };
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves all documents for a specific request ID.
        /// </summary>
        public IEnumerable<Document> GetDocumentsByRequestId(int requestId)
        {
            var documents = new List<Document>();

            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();
                var query = "SELECT DocumentID, RequestID, DocumentPath, UploadDate FROM Documents WHERE RequestID = @RequestID";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RequestID", requestId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            documents.Add(new Document
                            {
                                DocumentID = reader.GetInt32(0),
                                RequestID = reader.GetInt32(1),
                                DocumentPath = reader.GetString(2),
                                UploadDate = reader.GetDateTime(3)
                            });
                        }
                    }
                }
            }
            return documents;
        }

        /// <summary>
        /// Adds a new document to the database.
        /// </summary>
        public void AddDocument(Document document)
        {
            using (var connection = DatabaseContext.Instance.GetConnection())
            {
                connection.Open();

                var query = @"INSERT INTO Documents (RequestID, DocumentPath) 
                              VALUES (@RequestID, @DocumentPath);
                              SELECT SCOPE_IDENTITY();"; // Returns the newly inserted DocumentID

                using (var command = new SqlCommand(query, connection))
                {
                    // Ensure RequestID exists to prevent foreign key errors
                    if (document.RequestID <= 0)
                    {
                        throw new ArgumentException("Invalid RequestID. Document must be linked to a valid request.");
                    }

                    command.Parameters.AddWithValue("@RequestID", document.RequestID);
                    command.Parameters.AddWithValue("@DocumentPath", document.DocumentPath);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        document.DocumentID = Convert.ToInt32(result);
                    }
                    else
                    {
                        throw new Exception("Failed to insert document.");
                    }
                }
            }
        }
    }
}
