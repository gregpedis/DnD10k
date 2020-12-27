using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD10k.V1.Services.Interfaces
{
    public interface IDBService : IDisposable
    {
        public int Execute(string query, object queryParams = null);
        public IEnumerable<T> Query<T>(string query, object queryParams = null);
        public T QueryFirst<T>(string query, object queryParams = null);
        public T QueryFirstOrDefault<T>(string query, object queryParams = null);
    }
}

