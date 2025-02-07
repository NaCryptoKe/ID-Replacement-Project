using Microsoft.Data.SqlClient;

namespace ID_Replacement.Data
{
    /*
     *This class is public because it's intended to be used through out the project
     *This is a sealed class, which basically means you can't inherit from this class because we don't want it to be overriden by being inherited
     */
    public sealed class DatabaseContext
    {
        /*
         * This is a singleton implementaion for the database connection
         * Static: Shared across the application. Read-only: Can only be assigned once, ensuring the instance cannot be reassigned.
         * Lazy<T>
         * Ensures the instance is created only when it is first accessed. This is called lazy initialization.
         * Avoids unnecessary resource usage if DatabaseContext is never used during the application's lifecycle.
         * 
         * The lambda () => new DatabaseContext() specifies how the singleton instance is created: by invoking the private constructor.
         */
        private static readonly Lazy<DatabaseContext> _instance =
            new Lazy<DatabaseContext>(() => new DatabaseContext());

        private readonly string _connectionString;

        // Private constructor ensures no external instantiation
        private DatabaseContext()
        {
            _connectionString = "Replace with your database connection string";
        }

        /* 
         * Public property to access the single instance
         * Singleton Instance Property
         * public: Exposed to the rest of the application.
         * static: No need to create an object; you access it using DatabaseContext.Instance.
         * Instance: Returns the singleton instance of DatabaseContext stored in _instance
         * Lazy Evaluation:
         * _instance.Value: Evaluates the Lazy<T> object and initializes DatabaseContext the first time it’s accessed.
         */
        public static DatabaseContext Instance => _instance.Value;

        // Method to get a new SqlConnection
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}