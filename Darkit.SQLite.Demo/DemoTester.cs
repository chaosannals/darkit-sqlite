using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Darkit.Text;
using Darkit.SQLite.Data;

namespace Darkit.SQLite.Demo
{
    [Table("d_tester", ColumnCase=CaseStyle.SnakeStyle)]
    [Key("id", IsAutoIncrement = true)]
    [Index("name", IsUnique = true)]
    public class DemoTester
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Column(Default = "datetime('now', 'localtime')")]
        public DateTime EnterAt { get; set; }
        public DateTime? LeaveAt { get; set; }
    }
}
