using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DBSA2._0.ClassLibrary
{
    [Table("OwnLocations")]
    public class OwnLocations
    {
        [MaxLength(128), NotNull, Unique, PrimaryKey]
        public string location
        {
            get;
            set;
        }
    }
}
