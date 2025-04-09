using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Database
{
    [Table("Timeframe")]
    public class Timeframe
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("TimeframeID")]
        public int TimeframeID { get; set; }
        [Column("StartInt")]
        public int Start { get; set; }
        [Column("EndInt")]
        public int End { get; set; }
        public int Duration => End >= Start ? End - Start : (1440 - Start) + End;

    }
}
