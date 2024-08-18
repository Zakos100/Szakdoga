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

        [Column("UserName")]
         
        public string UserName { get; set; }

        [Column("UserPassword")]
        public string UserPassword { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Role")]
        public string Role { get; set; }
        [Column("DeviceID")]
        public string DeviceID { get; set; }


    }
}
