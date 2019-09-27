using MobileApp.Droid.Handlers;
using MobileApp.Handlers;
using SQLite;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(ISQLite_Android))]

namespace MobileApp.Droid.Handlers
{
    public class ISQLite_Android : ISQLite
    {
        public ISQLite_Android() { }
        public SQLiteConnection GetConnection()
        {
            string fileName = "Main.db3";
            string dir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(dir, fileName);

            return new SQLiteConnection(path);
        }
    }
}