using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darkit.SQLite.Query
{
    public partial class SQLiteQuery
    {
        public IEnumerable<T> Select<T>(params string[] fields)
        {
            yield return default(T);
        }

        public IEnumerable<T> Select<T, F>(F fields)
        {
            yield return default(T);
        }

        public IEnumerable<Dictionary<string, object>> Select(params string[] fields)
        {
            return new List<Dictionary<string, object>>();
        }

        public T Find<T>(params string[] fields)
        {
            return default(T);
        }

        public bool Has()
        {
            return false;
        }
    }
}
