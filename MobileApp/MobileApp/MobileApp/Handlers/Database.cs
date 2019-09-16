using MobileApp.Handlers;
using MobileApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileApp.Handlers
{
    public class Database
    {
        static readonly object locker = new object();
        readonly SQLiteConnection database;

        public Database()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<User>();
            if (database.Table<User>().Count() < 1)
                AddUser(new User("alpha", "test", "alphathekiwi@gmail.com", "+64 204 051 3343"));
        }

        public bool AddUser(User User)
        {
            lock (locker)
            {
                return database.Insert(User) > 0;
            };
        }

        public User GetUser(string userName)
        {
            lock (locker)
            {
                List<User> users = database.Query<User>($"SELECT * FROM User WHERE UserName='{userName}'");
                if (users.Count > 0)
                {
                    users[0].Password = null;
                    return users[0];
                }
                else return null;
            }
        }
        public User VerifyUser(string Username, string password)
        {
            lock (locker)
            {
                List<User> users = database.Query<User>($"SELECT * FROM User WHERE UserName='{Username}'");
                if (users.Count > 0 && users[0].Password == password)
                {
                    users[0].Password = null;
                    return users[0];
                }
                else return null;
            }
        }
    }
}
