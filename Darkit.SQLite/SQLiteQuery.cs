using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darkit.SQLite
{
    public partial class SQLiteQuery
    {
        public string Table { get; private set; }
        public SQLiteSession Session { get; private set; }

        public SQLiteQuery(SQLiteSession session, string table)
        {
            Session = session;
            Table = table;
        }

        public SQLiteQuery Where(string condition)
        {
            return this;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public partial class SQLiteQuery<T> where T : class
    {
        public SQLiteSession Session { get; private set; }

        public SQLiteQuery(SQLiteSession session)
        {
            Session = session;
        }

        public SQLiteQuery<T, J1> Join<J1>(Func<J1, bool> condition, string alias = null)
        {
            return new SQLiteQuery<T, J1>(Session);
        }

        public SQLiteQuery<T> Where(Func<T, bool> condition)
        {
            return this;
        }
    }

    public partial class SQLiteQuery<T, J1> : SQLiteQuery<T> where T : class
    {
        internal SQLiteQuery(SQLiteSession session):base(session) { }

        public SQLiteQuery<T, J1, J2> Join<J2>(Func<T, J1, J2, bool> condition, string alias=null)
        {
            return new SQLiteQuery<T, J1, J2>(Session);
        }

        public SQLiteQuery<T, J1> Where(Func<T, J1, bool> condition)
        {
            return this;
        }
    }

    public partial class SQLiteQuery<T, J1, J2> : SQLiteQuery<T, J1> where T: class
    {
        internal SQLiteQuery(SQLiteSession session) : base(session) { }

        public SQLiteQuery<T, J1, J2> Where(Func<T, J1, J2, bool> condition)
        {
            return this;
        }
    }
}
