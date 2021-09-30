using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darkit.SQLite.Query
{
    /// <summary>
    /// 更新请求
    /// </summary>
    public partial class SQLiteQuery
    {
        public int Update<T>(T one)
        {
            return 0;
        }

        public void Update<T>(IEnumerable<T> data)
        {

        }
    }

    /// <summary>
    /// 指定类型查询请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class SQLiteQuery<T>
    {
        public int Update(T one)
        {
            return 0;
        }

        public void Update(IEnumerable<T> data)
        {

        }
    }
}
