using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSA2._0.ClassLibrary
{
    public class Location
    {
        public Location(uint index, string location)
        {
            this.index = index;
            this.location = location;
        }
        public uint index
        {
            get;
            set;
        }
        public string location
        {
            get;
            set;
        }
    }
}
