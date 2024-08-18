using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Database
{
    [Table("Users")]
    public class Users
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("UserID")]

        public int UserID { get; set; }

        [Column("Username")]
         
        public string Username { get; set; }

        [Column("Password")]
        public string Password { get; set; }

        [Column("Fullname")]
        public string Fullname { get; set; }

        [Column("Role")]
        public string Role { get; set; }
        [Column("DeviceID")]
        public string DeviceID { get; set; }


    }
}
