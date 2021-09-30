using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darkit.SQLite.Query
{
    /// <summary>
    /// 插入请求。
    /// </summary>
    public partial class SQLiteQuery
    {
        public void Insert<T>(IEnumerable<T> data)
        {

        }

        public void Add<T>(T data)
        {

        }
    }

    /// <summary>
    /// 指定类型插入请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class SQLiteQuery<T>
    {
        public void Insert(IEnumerable<T> data)
        {

        }

        public void Add(T data)
        {
            
            Session.Execute($"INSERT INTO {TableName}() VALUES ()");
        }
    }
}
