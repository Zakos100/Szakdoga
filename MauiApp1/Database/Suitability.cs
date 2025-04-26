using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Database
{
    [Table("Suitability")]
    public class Suitability
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Suitability_ID")]
        public int SuitabilityID { get; set; }
        [Column("Device_type")]
        public string Device_type { get; set; }
        [Column("Ability_min")]
        public int Ability_min { get; set; }
    }
}
