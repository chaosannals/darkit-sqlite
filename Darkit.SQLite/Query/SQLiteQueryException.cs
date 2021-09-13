using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darkit.SQLite.Query
{
    public class SQLiteQueryException : SQLiteException
    {
        public SQLiteQueryException(string message) : base(message) { }
    }
}
