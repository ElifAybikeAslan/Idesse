using Idesse.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Idesse.Helper
{
    public class SQLiteManager
    {
        SQLiteConnection _sqliteconnection;

        public SQLiteManager()
        {
            
            _sqliteconnection = DependencyService.Get<ISQLiteConnection>().GetConnection();
            _sqliteconnection.CreateTable<DbModel>();
        }

        #region CRUD
        public int Insert(DbModel dbModel)
        {
            return _sqliteconnection.Insert(dbModel);
        }

        public int Update(DbModel dbModel)
        {
            return _sqliteconnection.Update(dbModel);
        }

        public int Delete(int Id)
        {
            return _sqliteconnection.Delete<DbModel>(Id);
        }

        public IEnumerable<DbModel> GetAll()
        {
            return _sqliteconnection.Table<DbModel>();
        }

        public DbModel Get(int Id)
        {
            return _sqliteconnection.Table<DbModel>().Where(x => x.Id == Id).FirstOrDefault();
        }

        public void Dispose()
        {
            _sqliteconnection.Dispose();
        }
        #endregion
    }
}
