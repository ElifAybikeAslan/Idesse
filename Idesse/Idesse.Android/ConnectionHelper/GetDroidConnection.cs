using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Idesse.Droid.ConnectionHelper;
using Idesse.Helper;
using Idesse.Models;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(GetDroidConnection))]
namespace Idesse.Droid.ConnectionHelper
{
    public class GetDroidConnection : ISQLiteConnection
    {
        public SQLiteConnection GetConnection()
        {
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = System.IO.Path.Combine(documentPath, App.DbName);
            var combined = Path.Combine(documentPath, path);
            var conn = new SQLite.SQLiteConnection(path);
            conn.Table<DbModel>();
            
            return conn;
        }
    }
}