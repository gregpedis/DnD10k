using Dapper;
using DnD10k.Extensions;
using DnD10k.V1.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD10k.V1.Services
{
    public class DBProvider : IDBService
    {
        private string _database = "dnd10k.db";
        private SQLiteConnection _connection;

        public DBProvider()
        {
            var fullPath = Path.Combine(Environment.CurrentDirectory, _database);
            _connection = new SQLiteConnection($"Data Source={fullPath};Version=3;");
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public int Execute(string query, object queryParams = null)
        {
            var result = _connection.Execute(query, queryParams);
            return result;
        }

        public IEnumerable<T> Query<T>(string query, object queryParams = null)
        {
            var result = _connection.Query<T>(query, queryParams);
            return result;
        }

        public T QueryFirst<T>(string query, object queryParams = null)
        {
            var result = _connection.QueryFirst<T>(query, queryParams);
            return result;
        }

        public T QueryFirstOrDefault<T>(string query, object queryParams = null)
        {
            var result = _connection.QueryFirstOrDefault<T>(query, queryParams);
            return result;
        }
    }
}

