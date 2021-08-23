﻿using System;
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
        public uint index
        {
            get;
            set;
        }
        [MaxLength(128), NotNull, Unique]
        public string location
        {
            get;
            set;
        }
    }
}
