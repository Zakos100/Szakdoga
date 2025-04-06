using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Database
{
    [Table("Tasks")]
    public class Tasks
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("TaskID")]

        public int TaskID { get; set; }

        [Column("Planned_date")]

        public int Planned_date { get; set; }

        [Column("Deadline")]
        public string Deadline { get; set; }

        [Column("DeviceID")]
        public string DeviceID { get; set; }

        [Column("ResourceID")]
        public int ResourceID { get; set; }

        [Column("TimeframeID")]
        public int TimeframeID { get; set; }

        [Column("SuitabilityID")]
        public int SuitabilityID { get; set; }

        [Column("OperationTime")]
        public int OperationTime { get; set; }


    }
}
