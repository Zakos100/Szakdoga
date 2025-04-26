using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Database
{
    [Table("Timeframes")]
    public class Timeframes
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Timeframe_ID")]
        public int TimeframeID { get; set; }
        [Column("StartInt")]
        public int Start { get; set; }
        [Column("EndInt")]
        public int End { get; set; }
        public int Duration => End >= Start ? End - Start : (1440 - Start) + End;

    }
}
