using Org.BouncyCastle.Asn1.Cmp;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace MauiApp1.Database
{
    public class LocalDBService
    {

        private readonly SQLiteConnection _connection;

        private SQLiteAsyncConnection _database;

        public LocalDBService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
        }

        public async Task InitializeAsync()
        {
            await _database.CreateTableAsync<Tasks>();
            await _database.CreateTableAsync<Users>();
            await _database.CreateTableAsync<Timeframe>();
            await _database.CreateTableAsync<Suitability>();
            await _database.CreateTableAsync<Resources>();
            await _database.CreateTableAsync<Database.Device>();
            await _database.CreateTableAsync<UserTimeframes>();
        }
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
        public Task<List<Tasks>> GetTasksAsync()
        {
            return _database.Table<Tasks>().ToListAsync();
        }

        public Task<List<Users>> GetUsersAsync()
        {
            return _database.Table<Users>().ToListAsync();
        }

        public Task<List<Timeframe>> GetTimeframesAsync()
        {
            return _database.Table<Timeframe>().ToListAsync();
        }

        public Task<List<Suitability>> GetSuitabilitiesAsync()
        {
            return _database.Table<Suitability>().ToListAsync();
        }

        public Task<List<Resources>> GetResourcesAsync()
        {
            return _database.Table<Resources>().ToListAsync();
        }

        public Task<List<Device>> GetDevicesAsync()
        {
            return _database.Table<Device>().ToListAsync();
        }

        public Task<List<UserTimeframes>> GetUserTimeframesAsync()
        {
            return _database.Table<UserTimeframes>().ToListAsync();
        }



    }


}
