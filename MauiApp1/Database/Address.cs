using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Database
{
    [Table("Address")]
    public class Address
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("Address_ID")]

        public int AddressID { get; set; }

        [Column("Postcode")]

        public int Postcode { get; set; }

        [Column("City")]
        public string City { get; set; }

        [Column("Street")]
        public string Street { get; set; }

        [Column("House_number")]
        public int House_number { get; set; }
        [Column("Floor")]
        public int Floor { get; set; }

        [Column("Door")]
        public int Door { get; set; }


    }
}
