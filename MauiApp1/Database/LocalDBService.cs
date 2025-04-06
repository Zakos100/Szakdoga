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

        private readonly SQLiteConnection _connection;

        public static string DB_Name { get; } = System.IO.Path.Combine("D:\\egyetem\\VS\\repos\\MauiApp1\\MauiApp1", "WorkersDB");

        public LocalDBService()
        {
            _connection = new SQLiteConnection(DB_Name);
        }

        public List<Users> ListUsers()
        {
            return _connection.Table<Users>().ToList();
           
        }

        public Users GetUser(string username)
        {
            return _connection.Table<Users>().ToList().Where(u => u.Username == username).FirstOrDefault();
        } 

        public List<Device> ListDevices()
        {
            return _connection.Table<Device>().ToList();

        }



    }


}
