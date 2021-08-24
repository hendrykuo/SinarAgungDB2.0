using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace DBSA2._0.ClassLibrary
{
    [Table("Customer")]
    class Customer
    {
        [Unique, PrimaryKey]
        public string name
        {
            get;
            set;
        }
    }
    class CustomerUI
    { 
        public string name
        {
            get;
            set;
        }
        public uint index
        {
            get;
            set;
        }
    }
}
