using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Database
{
    [Table("UserTimeframes")]
    public class UserTimeframes
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("UserID")]

        public int UserID { get; set; }

        [Column("TimeframeID")]

        public int TimeFrameID { get; set; }
    }
}
