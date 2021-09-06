using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Data;
using System.Data.SQLite;
using Darkit.SQLite.Properties;

namespace Darkit.SQLite
{
    /// <summary>
    /// 
    /// </summary>
    public class SQLiteSession : IDisposable
    {
        public string FilePath { get; private set; }
        public SQLiteConnection Connection { get; private set; }
        public SQLiteSession(string filePath)
        {
            FilePath = filePath;
            Connection = null;
        }

        public int Execute(string sql, params SQLiteParameter[] args)
        {
            using (SQLiteCommand command = NewCommand(sql, args))
            {
                return command.ExecuteNonQuery();
            }
        }

        public int Execute<T>(string sql, T args)
        {
            return Execute(sql, args.GetType().GetProperties().Select(i => new SQLiteParameter
            {
                ParameterName = i.Name,
                Value = i.GetValue(args, null),
            }));
        }

        public SQLiteCommand NewCommand<T>(string sql, T args)
        {
            return NewCommand(sql, args.GetType().GetProperties().Select(i => new SQLiteParameter
            {
                ParameterName = i.Name,
                Value = i.GetValue(args, null),
            }));
        }

        public SQLiteCommand NewCommand(string sql, params SQLiteParameter[] args)
        {
            EnsureConnection();
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = Connection;
            command.CommandType = CommandType.Text;
            command.CommandText = sql;
            command.Parameters.AddRange(args);
            return command;
        }

        /// <summary>
        /// 确保动态库文件存在。
        /// </summary>
        public void EnsureDll()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQLite.Interop.dll");
            if (!File.Exists(path))
            {
                string prefix = Path.GetFileNameWithoutExtension(path).Replace('.', '_');
                string tag = IntPtr.Size == 8 ? "x64" : "x86";
                string field = string.Format("{0}_{1}", prefix, tag);
                byte[] data = Resources.ResourceManager.GetObject(field) as byte[];
                File.WriteAllBytes(path, data);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void EnsureConnection()
        {
            EnsureDll();

            if (Connection == null)
            {
                Connection = new SQLiteConnection(string.Format("Data Source={0}", FilePath));
            }
            switch (Connection.State)
            {
                case ConnectionState.Broken:
                    Connection.Close();
                    Connection.Open();
                    break;
                case ConnectionState.Closed:
                    Connection.Open();
                    break;
            }
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class SQLiteDesignStatement
    {
        public static SQLiteDesign The(this SQLiteSession session, string table)
        {
            return new SQLiteDesign(session, table);
        }

        public static void Create(this SQLiteSession session, string table, params string[] definded)
        {
            string designs = string.Join(",", definded);
            string sql = string.Format("CREATE TABLE {0} ({1})", table, designs);
            session.Execute(sql);
        }

        public static void AddIndex(this SQLiteSession session, string table, string name, params string[] fields)
        {
            string field = string.Join(",", fields.Select(i => string.Format("[{0}]", i)).ToArray());
            string sql = string.Format("CREATE INDEX {1} ON {0} ({2})", table, name, field);
            session.Execute(sql);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class SQLiteQueryStatement
    {
        public static SQLiteQuery From(this SQLiteSession session,  string table)
        {
            return new SQLiteQuery(session);
        }
    }
}
