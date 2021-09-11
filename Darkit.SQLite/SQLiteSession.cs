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
    /// 会话。
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

        /// <summary>
        /// 执行。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int Execute(string sql, params SQLiteParameter[] args)
        {
            using (SQLiteCommand command = NewCommand(sql, args))
            {
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 执行。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public int Execute<T>(string sql, T args)
        {
            return Execute(sql, args.GetType().GetProperties().Select(i => new SQLiteParameter
            {
                ParameterName = i.Name,
                Value = i.GetValue(args, null),
            }));
        }

        /// <summary>
        /// 创建一个命令。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public SQLiteCommand NewCommand<T>(string sql, T args)
        {
            return NewCommand(sql, args.GetType().GetProperties().Select(i => new SQLiteParameter
            {
                ParameterName = i.Name,
                Value = i.GetValue(args, null),
            }));
        }

        /// <summary>
        /// 创建命令。
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
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
        /// 确保连接。
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

        /// <summary>
        /// 回收资源。
        /// </summary>
        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
