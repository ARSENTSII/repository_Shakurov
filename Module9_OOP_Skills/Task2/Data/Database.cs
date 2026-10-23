using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Data
{
    public static class Database
    {
        private const string DbFile = "tasktracker.db";
        private static readonly string ConnectionString =$"Data Source={DbFile};Version=3;Foreign Keys=True;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }
    }
}
