using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefineX_Odeme_Sistemi_Forms_Odevi.Attributes
{
    internal class SqlMappingAttribute : Attribute
    {
        public string ColumnName { get; }

        public bool IsPrimaryKey { get; set; }

        public SqlMappingAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }
}
