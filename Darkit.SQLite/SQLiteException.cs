using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darkit.SQLite
{
    public class SQLiteException : Exception
    {
        public SQLiteException(string message):base(message)
        {

        }
    }
}
