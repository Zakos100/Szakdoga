using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Database
{
    [Table("Data")]
    public class Data
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("IDcard_number")]

        public int IDcard_number { get; set; }

        [Column("Mothersname")]

        public string Mothersname { get; set; }

        [Column("Place_of_birth")]
        public string Place_of_birth { get; set; }

        [Column("Date_of_birth")]
        public string Date_of_birth { get; set; }

        [Column("Phone_number")]
        public string Phone_number { get; set; }


    }
}
