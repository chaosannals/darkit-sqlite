using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darkit.SQLite.Data
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class IndexAttribute : Attribute
    {
        public string Name { get; set; }
        public bool IsUnique { get; set; }
        public string[] Columns { get; private set; }
        public IndexAttribute(string first, params string[] other)
        {
            Columns = new string[other.Length + 1];
            Columns[0] = first;
            Array.Copy(other, 0, Columns, 1, other.Length);
            Name = string.Join("_", Columns.Select(c => c.ToUpper()).ToArray()) + "_INDEX";
            IsUnique = false;
        }

        public string ToSQL(string table)
        {
            string field = string.Join(",", Columns.Select(i => string.Format("[{0}]", i)).ToArray());
            string flag = IsUnique ? "UNIQUE" : string.Empty;
            return $"CREATE {flag} INDEX {Name} ON {table} ({field})";
        }
    }
}
