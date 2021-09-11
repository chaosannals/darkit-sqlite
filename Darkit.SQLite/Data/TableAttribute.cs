using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Darkit.Text;

namespace Darkit.SQLite.Data
{
    public enum ColumnCaseStyle
    {
        RawCase = 0,
        SnakeCase,
        KebabCase,
        CamelCase,
        PascalCase,
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : Attribute
    {
        public string Name { get; private set; }
        public CaseStyle ColumnCase { get; set; }

        public TableAttribute(string name)
        {
            Name = name;
            ColumnCase = CaseStyle.RawStyle;
        }
    }
}
