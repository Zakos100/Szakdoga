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
        [Column("User_ID")]

        public int UserID { get; set; }

        [Column("Timeframe_ID")]

        public int TimeFrameID { get; set; }
    }
}
