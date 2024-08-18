using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Database
{
    public class LocalDBService
    {

        private readonly SQLiteConnection _connetion;

        public static string DB_Name { get; } = System.IO.Path.Combine("D:\\egyetem\\VS\\repos\\MauiApp1\\MauiApp1", "WorkersDB");


        public LocalDBService()
        {
            _connetion = new SQLiteConnection(DB_Name);
        }

        public List<Users> ListUsers()
        {
            return _connetion.Table<Users>().ToList();
        }

        

    }


}
