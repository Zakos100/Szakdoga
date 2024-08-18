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
        public static string DB_Name { get; }
        = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WorkersDB.db");
        

        public LocalDBService()
        {
            _connetion = new SQLiteConnection(DB_Name);
            _connetion.CreateTable<Users>();
        }

        public List<Users> List()
        {
            return _connetion.Table<Users>().ToList();
        }

        

    }


}
