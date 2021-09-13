using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darkit.SQLite.Data
{
    /// <summary>
    /// 主键
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class KeyAttribute : Attribute
    {
        public string[] Columns { get; private set; }
        public bool IsAutoIncrement { get; set; }

        public KeyAttribute(string first, params string[] other)
        {
            Columns = new string[other.Length + 1];
            Columns[0] = first;
            Array.Copy(other, 0, Columns, 1, other.Length);
            IsAutoIncrement = false;
        }

        public string ToSQLSegment()
        {
            string columns = string.Join(",", Columns);
            if (IsAutoIncrement)
            {
                if (Columns.Length > 1)
                {
                    throw new SQLiteException($"复合主键 ({columns}) 不可自增");
                }
                return string.Empty; // 自增主键，关键字在列定义。
            }
            return $"PRIMARY KEY ({columns})"; // 生成
        }
    }
}
