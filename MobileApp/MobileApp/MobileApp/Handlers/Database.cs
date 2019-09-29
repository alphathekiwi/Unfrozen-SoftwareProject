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
            database.DropTable<User>();
            database.CreateTable<User>();
            if (database.Table<User>().Count() < 1)
            {
                SaveUser(new User("alpha", "test", "alphathekiwi@gmail.com", "+64 204 051 3343") { FirstName = "Aaron", LastName = "Saunders"});
                SaveUser(new User("yara", "test", "test@email.com", "") { FirstName = "Yara", LastName = "Test" });
                SaveUser(new User("admin", "test", "", ""));
            }
            //database.DropTable<Issue>();
            database.CreateTable<Issue>();
            if (database.Table<Issue>().Count() < 1)
                SaveIssue(new Issue(1, "Love Testing", "Something to test that all my code is working correctly"));
            database.CreateTable<Comment>();

        }
        #region User methods
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
        #endregion
        #region Issue methods
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
        #endregion
        #region Comment methods
        public bool SaveComment(Comment Comment)
        {
            lock (locker)
            {
                return Comment.Id == 0 ? database.Insert(Comment) > 0 : database.InsertOrReplace(Comment) > 0;
            };
        }
        public List<Comment> GetComments(int issue, int author = 0)
        {
            lock (locker)
            {
                if (issue == 0) return database.Table<Comment>().ToList();
                else if(author > 0)
                    return database.Query<Comment>($"SELECT * FROM Comment WHERE IssueId='{issue}'&&Author=='{author}'");
                return database.Query<Comment>($"SELECT * FROM Comment WHERE IssueId='{issue}'");
            };
        }
        #endregion
    }
}
