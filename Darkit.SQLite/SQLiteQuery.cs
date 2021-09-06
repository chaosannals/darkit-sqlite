using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darkit.SQLite
{
    /// <summary>
    /// 
    /// </summary>
    public class SQLiteQueryException : SQLiteException
    {
        public SQLiteQueryException(string message) : base(message) { }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SQLiteQuery
    {
        public SQLiteSession Session { get; private set; }

        public SQLiteQuery(SQLiteSession session)
        {
            Session = session;
        }

        public SQLiteQuery Where(string condition)
        {
            return this;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class SQLiteSelectStatement
    {
        public static IEnumerable<T> Select<T>(this SQLiteQuery query, params string[] fields)
        {
            yield return default(T);
        }

        public static IEnumerable<T> Select<T, F>(this SQLiteQuery query, F fields)
        {
            yield return default(T);
        }

        public static IEnumerable<Dictionary<string, object>> Select(this SQLiteQuery query, params string[] fields)
        {
            return new List<Dictionary<string, object>>();
        }

        public static T Find<T>(this SQLiteQuery query, params string[] fields)
        {
            return default(T);
        }

        public static bool Has(this SQLiteQuery query)
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class SQLiteInsertStatement
    {
        public static void Insert<T>(this SQLiteQuery query, IEnumerable<T> data)
        {

        }

        public static void Add<T>(this SQLiteQuery query, T data)
        {

        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class SQLiteUpdateStatement
    {
        public static int Update<T>(this SQLiteQuery query, T one)
        {
            return 0;
        }

        public static void Update<T>(this SQLiteQuery query, IEnumerable<T> data)
        {

        }
    }
}
