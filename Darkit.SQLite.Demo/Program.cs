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
                session.The("tester")
                    .Column(new SQLiteDesignColumn
                    {
                        Name = "id",
                        Kind = SQLiteDesignColumnKind.INTEGER,
                        IsPrimaryAutoIncrement = true,
                    })
                    .Column(new SQLiteDesignColumn
                    {
                        Name = "account",
                        Kind = SQLiteDesignColumnKind.STRING,
                    })
                    .Column(new SQLiteDesignColumn
                    {
                        Name = "gender",
                        Kind = SQLiteDesignColumnKind.INTEGER,
                        DefaultValue = "0"
                    })
                    .Column(new SQLiteDesignColumn
                    {
                        Name = "create_at",
                        Kind = SQLiteDesignColumnKind.DATETIME
                    })
                    .Unique("account")
                    .Create();
                session.From("tester");
            }

            Console.ReadKey();
        }
    }
}
