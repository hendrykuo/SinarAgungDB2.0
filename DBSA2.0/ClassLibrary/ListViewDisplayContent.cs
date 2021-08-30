using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSA2._0.ClassLibrary
{
    public class ListViewDisplayContent
    {
        public ListViewDisplayContent(int index, string name, string message)
        {
            this.index = index;
            this.name = name;
            this.message = message;
        }
        public ListViewDisplayContent(int index, string name)
        {
            this.index = index;
            this.name = name;
            this.message = string.Empty;
        }
        public int index { get; set; }
        public string name { get; set; }
        public string message { get; set; }
    }
}
