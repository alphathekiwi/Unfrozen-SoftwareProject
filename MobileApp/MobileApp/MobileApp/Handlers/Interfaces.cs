using SQLite;
using System.IO;
using System.Threading.Tasks;

namespace MobileApp.Handlers
{
    public interface ISQLite
    {
        // interface used by each platform to get platform specific SQL accessors
        SQLiteConnection GetConnection();
    }
    public interface IPhotoPick
    {
        Task<Stream> GetImageStreamAsync();
    }
}