using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darkit.SQLite.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SQLiteSession session = new SQLiteSession("demo.db"))
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

            Console.ReadKey();
        }
    }
}
