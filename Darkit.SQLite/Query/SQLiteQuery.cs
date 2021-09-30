using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Darkit.Text;
using Darkit.SQLite.Data;

namespace Darkit.SQLite.Query
{
    /// <summary>
    /// 查询基类
    /// </summary>
    public partial class SQLiteQuery
    {
        public string TableName { get; private set; }
        public SQLiteSession Session { get; private set; }

        public SQLiteQuery(SQLiteSession session, string table)
        {
            Session = session;
            TableName = table;
        }

        public SQLiteQuery Where(string condition)
        {
            return this;
        }
    }

    /// <summary>
    /// 查询基类，单表
    /// </summary>
    public partial class SQLiteQuery<T> where T : class
    {
        public string TableName { get; private set; }
        public TableAttribute Table { get; private set; }
        public SQLiteSession Session { get; private set; }
        public string Conditions { get; private set; }

        public SQLiteQuery(SQLiteSession session)
        {
            Session = session;
            Type type = typeof(T);
            foreach (object a in type.GetCustomAttributes(true))
            {
                if (a is TableAttribute ta)
                {
                    Table = ta;
                }
            }
            TableName = Table.Name ?? type.Name;
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

    /// <summary>
    /// 联表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="J1"></typeparam>
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

    /// <summary>
    /// 联表2
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="J1"></typeparam>
    /// <typeparam name="J2"></typeparam>
    public partial class SQLiteQuery<T, J1, J2> : SQLiteQuery<T, J1> where T: class
    {
        internal SQLiteQuery(SQLiteSession session) : base(session) { }

        public SQLiteQuery<T, J1, J2> Where(Func<T, J1, J2, bool> condition)
        {
            return this;
        }
    }
}
