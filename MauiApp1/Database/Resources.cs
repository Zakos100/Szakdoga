﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Database
{
    [Table("Resources")]
    public class Resources
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Resource_ID")]
        public int ResourceID { get; set; }

        [Column("Ability_req")]
        public int Ability_req { get; set; }
    }
}
