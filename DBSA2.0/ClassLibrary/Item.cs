using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace DBSA2._0.ClassLibrary
{
    [Table("Item")]
    public class Item
    {
        [PrimaryKey, NotNull, AutoIncrement,]
        public uint index
        {
            get;
            set;
        
        }
    }
}
