using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;

namespace Darkit.SQLite
{
    public enum SQLiteDesignColumnKind
    {
        BOOLEAN,
        INTEGER,
        STRING,
        DATETIME,
    }

    public class SQLiteDesignColumn
    {
        public string Name { get; set; }
        public SQLiteDesignColumnKind Kind { get; set; }
        public string DefaultValue { get; set; }
        public bool IsNotNull { get; set; }
        public bool IsPrimaryAutoIncrement { get; set; }
        

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
            if (!string.IsNullOrEmpty(DefaultValue))
            {
                infos.Add(string.Format("DEFAULT({0})", DefaultValue));
            }

            return string.Join(" ", infos.ToArray());
        }
    }

    public class SQLiteDesign
    {
        public string Table { get; private set; }
        public SQLiteSession Session { get; private set; }
        public List<SQLiteDesignColumn> Columns { get; private set; }
        public List<string> PrimaryKeys { get; private set; }
        public List<List<string>> UniqueIndexes { get; private set; }
        public List<List<string>> Indexes { get; private set; }

        public SQLiteDesign(SQLiteSession session, string table)
        {
            Table = table;
            Session = session;
            Columns = new List<SQLiteDesignColumn>();
            PrimaryKeys = new List<string>();
            UniqueIndexes = new List<List<string>>();
            Indexes = new List<List<string>>();
        }

        public SQLiteDesign Column(SQLiteDesignColumn column)
        {
            Columns.Add(column);
            return this;
        }

        public SQLiteDesign Primary(string name, params string[] others)
        {
            PrimaryKeys.Add(string.Format("[{0}]", name));
            PrimaryKeys.AddRange(others.Select(i => string.Format("[{0}]", i)));
            return this;
        }

        public SQLiteDesign Unique(string name, params string[] others)
        {
            List<string> indexes = new List<string>();
            indexes.Add(string.Format("[{0}]", name));
            indexes.AddRange(others.Select(i => string.Format("[{0}]", i)));
            UniqueIndexes.Add(indexes);
            return this;
        }
    }

    public static class SQLiteCreateStatement
    {
        public static bool Exists(this SQLiteDesign design)
        {
            string sql = "SELECT count(*) FROM sqlite_master WHERE type='table' AND name = @table";
            using (SQLiteCommand command = design.Session.NewCommand(sql, new SQLiteParameter
            {
                ParameterName = "table",
                Value = design.Table,
            }))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    return reader.Read();
                }
            }
        }

        public static void Create(this SQLiteDesign design)
        {
            List<string> cd = new List<string>();
            cd.AddRange(design.Columns.Select(i => i.ToString()));
            if (design.PrimaryKeys.Count > 0)
            {
                cd.Add(string.Format("PRIMARY KEY ({0})", string.Join(",", design.PrimaryKeys.ToArray())));
            }
            cd.AddRange(design.UniqueIndexes.Select(i => string.Format("UNIQUE({0})", string.Join(",", i.ToArray()))));
            string sql = string.Format(
                "CREATE TABLE {0} ({1})",
                design.Table,
                string.Join(",", cd.ToArray())
            );
            // Console.WriteLine(sql);
            design.Session.Execute(sql);
        }
    }

    public static class SQLiteAlterStatment
    {
        public static void Rename(this SQLiteDesign design, string name)
        {
            string sql = string.Format("ALTER TABLE {0} RENAME TO {1}", design.Table, name);
            design.Session.Execute(sql);
        }
    }
}
