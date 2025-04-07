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
        [Column("Start")]
        public TimeSpan Start { get; set; }
        [Column("End")]
        public TimeSpan End { get; set; }
        public int Duration => (int)(End - Start).TotalMinutes; 
    }
}
