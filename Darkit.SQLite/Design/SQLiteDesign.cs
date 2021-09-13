using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Darkit.SQLite.Data;

namespace Darkit.SQLite.Design
{
    public class SQLiteDesign
    {
        public Type TableType { get; set; }
        public string TableName { get; set; }
        public TableAttribute Table { get; set; }
        public KeyAttribute Key { get; set; }
        public List<SQLiteDesignColumn> Columns { get; private set; }
        public List<IndexAttribute> Indexes { get; private set; }

        public SQLiteDesign()
        {
            TableType = null;
            TableName = null;
            Table = null;
            Key = null;
            Columns = new List<SQLiteDesignColumn>();
            Indexes = new List<IndexAttribute>();
        }

        public string GetTableName()
        {
            return Table?.Name ?? TableName ?? TableType?.Name;
        }

        public string[] GetDefinitions()
        {
            List<string> definitions = new List<string>();

            definitions.AddRange(Columns.Select(i => i.ToString()));
            definitions.Add(Key?.ToSQLSegment());

            return definitions.Where(i => !string.IsNullOrEmpty(i)).ToArray();
        }
    }
}
