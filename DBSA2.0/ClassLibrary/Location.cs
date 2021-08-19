using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DBSA2._0.ClassLibrary
{
    [Table("Location")]
    public class Location
    {
        [PrimaryKey, Column("index")]
        public uint index
        {
            get;
            set;
        }
        [MaxLength(128), Unique]
        public string location
        {
            get;
            set;
        }
        public bool IsOwnedLocation
        {
            get;
            set;
        }
    }
}
