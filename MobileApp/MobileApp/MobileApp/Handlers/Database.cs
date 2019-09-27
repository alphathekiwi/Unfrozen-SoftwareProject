using MobileApp.Models;
using SQLite;
using System.Collections.Generic;
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
            //database.DropTable<User>();
            database.CreateTable<User>();
            if (database.Table<User>().Count() < 1)
            {
                AddUser(new User("alpha", "test", "alphathekiwi@gmail.com", "+64 204 051 3343"));
                AddUser(new User("yara", "test", "", ""));
                AddUser(new User("admin", "test", "", ""));
            }
            database.CreateTable<Issue>();
            if (database.Table<Issue>().Count() < 1)
                AddIssue(new Issue(1, "Love Testing", "Something to test that all my code is working correctly"));
        }

        public bool AddUser(User User)
        {
            lock (locker)
            {
                return database.Insert(User) > 0;
            };
        }
        public bool AddIssue(Issue Issue)
        {
            lock (locker)
            {
                return database.Insert(Issue) > 0;
            };
        }
        public bool SaveUser(User User)
        {
            lock (locker)
            {
                if (User.Id == 0) return database.Insert(User) > 0;
                User u = database.Query<User>($"SELECT * FROM User WHERE Id='{User.Id}'")[0];
                if (u != null && (User.Password == null || User.Password == ""))
                    User.Password = u.Password;
                return database.InsertOrReplace(User) > 0;
            };
        }
        public bool SaveIssue(Issue Issue)
        {
            lock (locker)
            {
                return Issue.Id == 0 ? database.Insert(Issue) > 0 : database.InsertOrReplace(Issue) > 0;
            };
        }
        public List<Issue> GetIssues(int id)
        {
            lock (locker)
            {
                if (id == 0) return database.Table<Issue>().ToList();
                return database.Query<Issue>($"SELECT * FROM Issue WHERE Author='{id}'");
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
        public User GetUser(int id)
        {
            lock (locker)
            {
                List<User> users = database.Query<User>($"SELECT * FROM User WHERE Id='{id}'");
                if (users.Count > 0)
                {
                    users[0].Password = null;
                    return users[0];
                }
                return null;
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
