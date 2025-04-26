using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Database
{
    [Table("Devices")]
    public class Devices
    {
        [PrimaryKey]
        [Column("Device_ID")]

        public string DeviceID { get; set; }

        [Column("Device_name")]

        public string Device_name { get; set; }

        [Column("Device_type")]
        public string Device_type { get; set; }

        [Column("MAC_address")]
        public string MAC_address { get; set; }

        [Column("Last_update")]
        public string Last_update { get; set; }
        


    }
}
