using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Darkit.SQLite.Data;

namespace Darkit.SQLite.Demo
{
    [Table("d_tester_book")]
    [Key("tester_id", "book_id")]
    public class DemoTesterBook
    {
        public int TesterId { get; set; }
        public int BookId { get; set; }

        [Column(Default = "datetime('now', 'localtime')")]
        public DateTime CreateAt { get; set; }
    }
}
