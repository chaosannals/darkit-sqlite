using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Data.SQLite;
using Darkit.SQLite.Data;
using Darkit.SQLite.Design;

namespace Darkit.SQLite
{
    /// <summary>
    /// 
    /// </summary>
    public static class SQLiteDesignStatement
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        public static void Create<T>(this SQLiteSession session)
        {
            Type type = typeof(T);
            SQLiteDesign design = new SQLiteDesign();
            foreach (object a in type.GetCustomAttributes(true))
            {
                Console.WriteLine(a);
                if (a is TableAttribute ta)
                {
                    design.Table = ta;
                }
                else if (a is KeyAttribute ka)
                {
                    design.Key = ka;
                }
                else if (a is IndexAttribute ia)
                {
                    design.Indexes.Add(ia);
                }
            }
            foreach (PropertyInfo pi in type.GetProperties())
            {
                Console.WriteLine(pi.Name);
                design.Columns.Add(SQLiteDesignColumn.Parse(pi));
            }

        }

        /// <summary>
        /// 创建表。
        /// </summary>
        /// <param name="session"></param>
        /// <param name="table"></param>
        /// <param name="definded"></param>
        public static void CreateTable(this SQLiteSession session, string table, params string[] definded)
        {
            string designs = string.Join(",", definded);
            string sql = string.Format("CREATE TABLE {0} ({1})", table, designs);
            session.Execute(sql);
        }

        /// <summary>
        /// 添加索引。
        /// </summary>
        /// <param name="session"></param>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <param name="fields"></param>
        public static void CreateIndex(this SQLiteSession session, string table, string name, bool isUnique, params string[] fields)
        {
            string field = string.Join(",", fields.Select(i => string.Format("[{0}]", i)).ToArray());
            string flag = isUnique ? "UNIQUE" : string.Empty;
            string sql = $"CREATE {flag} INDEX {name} ON {table} ({field})";
            session.Execute(sql);
        }

        /// <summary>
        /// 重命名表。
        /// </summary>
        /// <param name="session"></param>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static int RenameTable(this SQLiteSession session, string table, string name)
        {
            string sql = string.Format("ALTER TABLE {0} RENAME TO {1}", table, name);
            return session.Execute(sql);
        }

        /// <summary>
        /// 判定表存在。
        /// </summary>
        /// <param name="session"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ExistsTable(this SQLiteSession session, string name)
        {
            string sql = "SELECT count(*) FROM [sqlite_master] WHERE [type]='table' AND [name] = @table";
            SQLiteParameter parameter = new SQLiteParameter
            {
                ParameterName = "table",
                Value = name,
            };
            using (SQLiteCommand command = session.NewCommand(sql, parameter))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    return reader.Read();
                }
            }
        }
    }
}
