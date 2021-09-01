using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace DBSA2._0.ClassLibrary
{
    class ItemData
    {
        [Unique, NotNull, PrimaryKey]
        public string barcode
        {
            get;
            set;
        }
        [NotNull]
        public string time
        {
            get;
            set;
        }
        [NotNull]
        public string location
        {
            get;
            set;
        }
    }
}
