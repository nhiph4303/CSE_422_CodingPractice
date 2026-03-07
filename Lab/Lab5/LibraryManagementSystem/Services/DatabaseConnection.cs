using System;

namespace LibraryManagementSystem.Services
{
    public class DatabaseConnection
    {
        private static DatabaseConnection _instance;
        private static readonly object _lock = new object();

        private DatabaseConnection()
        {
            Console.WriteLine("[Singleton] Database connection established.");
        }

        public static DatabaseConnection GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DatabaseConnection();
                    }
                }
            }
            return _instance;
        }

        public void ExecuteQuery(string sql)
        {
            Console.WriteLine($"[Database] Executing: {sql}");
        }
    }
}
