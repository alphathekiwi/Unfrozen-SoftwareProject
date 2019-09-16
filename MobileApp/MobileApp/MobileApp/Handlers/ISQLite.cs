using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MobileApp.Handlers
{
    public interface ISQLite
    {
        // interface used by each platform to get platform specific SQL accessors
        SQLiteConnection GetConnection();
    }
}