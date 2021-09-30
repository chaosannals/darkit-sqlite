using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Darkit.SQLite.Data;

namespace Darkit.SQLite.Design
{
    /// <summary>
    /// 表设计列信息
    /// </summary>
    public class SQLiteDesignColumn
    {
        public string Name { get; set; }
        public string Kind { get; set; }
        public string Default { get; set; }
        public bool IsNotNull { get; set; }
        public bool IsPrimaryAutoIncrement { get; set; }

        /// <summary>
        /// 生成 SQL 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            List<string> infos = new List<string> { string.Format("[{0}] {1}", Name, Kind) };
            if (IsPrimaryAutoIncrement)
            {
                infos.Add("PRIMARY KEY AUTOINCREMENT");
            }
            if (IsNotNull)
            {
                infos.Add("IS NOT NULL");
            }
            if (!string.IsNullOrEmpty(Default))
            {
                infos.Add(string.Format("DEFAULT({0})", Default));
            }

            return string.Join(" ", infos.ToArray());
        }

        /// <summary>
        /// 解析属性信息获取列信息。（不完整，还有部分信息从总体上获取）
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public static SQLiteDesignColumn Parse(PropertyInfo pi)
        {
            SQLiteDesignColumn result = new SQLiteDesignColumn();
            result.Name = pi.Name;
            result.IsNotNull = false;
            foreach (object a in pi.GetCustomAttributes(true))
            {
                if (a is ColumnAttribute ca)
                {
                    result.Name = ca.Name ?? pi.Name;
                    result.IsNotNull = ca.NullStatus ?? result.IsPrimaryAutoIncrement;
                    break;
                }
            }

            // 获得类型
            if (pi.PropertyType == typeof(string))
            {
                result.Kind = "STRING";
            }
            else if (new Type[] { typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(short?), typeof(ushort?), typeof(int?), typeof(uint?), typeof(long?), typeof(ulong?) }.Contains(pi.PropertyType))
            {
                result.Kind = "INTEGER";
            }
            else if (new Type[] { typeof(bool), typeof(bool?) }.Contains(pi.PropertyType))
            {
                result.Kind = "BOOLEAN";
            }
            else if (new Type[] { typeof(DateTime), typeof(DateTime?) }.Contains(pi.PropertyType))
            {
                result.Kind = "DATETIME";
            }
            else if (new Type[] { typeof(decimal), typeof(float), typeof(double), typeof(decimal?), typeof(float?), typeof(double?) }.Contains(pi.PropertyType))
            {
                result.Kind = "NUMERIC";
            }
            return result;
        }
    }
}
