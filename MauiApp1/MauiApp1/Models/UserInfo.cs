using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class UserInfo
    {

        public string FullName { get; set; }

        public string Username { get; set; }

        public int RoleID { get; set; }
        public string RoleText { get; set; }


    }

    public enum RoleDetails
    {
        Worker = 1,
        Admin,
    }
}
