using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Darkit.SQLite.Data;

namespace Darkit.SQLite.Design
{
    public class SQLiteDesign
    {
        public TableAttribute Table { get; set; }
        public KeyAttribute Key { get; set; }
        public List<SQLiteDesignColumn> Columns { get; private set; }
        public List<IndexAttribute> Indexes { get; private set; }

        public SQLiteDesign()
        {
            Table = null;
            Key = null;
            Columns = new List<SQLiteDesignColumn>();
            Indexes = new List<IndexAttribute>();
        }

        public 
    }
}
