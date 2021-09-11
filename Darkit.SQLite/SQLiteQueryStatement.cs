using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darkit.SQLite
{
    /// <summary>
    /// 查询语句。
    /// </summary>
    public static class SQLiteQueryStatement
    {
        public static SQLiteQuery From(this SQLiteSession session, string table)
        {
            return new SQLiteQuery(session);
        }
    }
}
