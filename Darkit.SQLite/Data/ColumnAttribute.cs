using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Darkit.SQLite.Data
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColumnAttribute : Attribute
    {
        public string Name { get; set; }
        public string Default { get; set; }
        public bool IsNotNull { get; set; }

        public ColumnAttribute()
        {
            Name = null;
            Default = null;
            IsNotNull = true;
        }
    }
}
