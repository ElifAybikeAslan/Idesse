using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Idesse.Helper
{
    public interface ISQLiteConnection
    {
        SQLiteConnection GetConnection();
    }
}
