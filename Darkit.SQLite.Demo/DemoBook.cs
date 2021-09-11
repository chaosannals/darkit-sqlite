using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Darkit.Text;
using Darkit.SQLite.Data;

namespace Darkit.SQLite.Demo
{
    [Table("d_book", ColumnCase = CaseStyle.SnakeStyle)]
    [Key("id", IsAutoIncrement = true)]
    [Index("name", IsUnique = true)]
    [Index("up_at")]
    public class DemoBook
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Column(IsNotNull = false)]
        public string Author { get; set; }

        public decimal? Price { get; set; }
        public DateTime? UpAt { get; set; }
        public DateTime? DownAt { get; set; }
    }
}
