using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Darkit.SQLite.Design;

namespace Darkit.SQLite.Demo
{
    class Program
    {
        static void CreateDB(string dbpath)
        {
            if (File.Exists(dbpath))
            {
                File.Delete(dbpath);
            }
            using (SQLiteSession session = new SQLiteSession(dbpath))
            {
                session.CreateTable("tester", new string[]
                {
                    "id INTEGER NOT NULL",
                    "account STRING NOT NULL",
                    "gender INTEGER DEFAULT 0",
                    "create_at DATETIME DEFAULT (datetime('now','localtime'))",
                });
                session.Create<DemoBook>();
            }
        }

        static void Main(string[] args)
        {
            string dbpath = "demo.db";
            CreateDB(dbpath);

            using (SQLiteSession session = new SQLiteSession(dbpath))
            {
                session.From<DemoBook>()
                    .Add(new DemoBook
                    {
                        Name = "Book Foo"
                    });
            }
            
            Console.ReadKey();
        }
    }
}
