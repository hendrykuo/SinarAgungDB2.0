using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace DBSA2._0.ClassLibrary
{
    [Table("Customer")]
    public class Customer
    {
        [Unique, NotNull, PrimaryKey]
        public string name
        {
            get;
            set;
        }
    }
}
