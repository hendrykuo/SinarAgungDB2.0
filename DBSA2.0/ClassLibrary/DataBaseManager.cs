using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
//using SQLitePCL;
namespace DBSA2._0.ClassLibrary
{
    class DataBaseManager
    {
        const string dbName = "programDB.db";
        public DataBaseManager()
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            { 
                connection.CreateTable<ClassLibrary.OwnLocations>();
                InitialLocationSettup();
            }
        }
        public void InitialLocationSettup()
        {
            AddNewOwnedLocation(new OwnLocations() { location = "Gudang Margo"}) ;
            AddNewOwnedLocation(new OwnLocations() { location = "Gudang 65"});
        }
        public int AddNewOwnedLocation(OwnLocations location)
        {
            int result = int.MinValue;
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                try
                {
                    result = connection.Insert(location);
                }
                catch (Exception e)
                {
                    string exp = e.Message.ToString();
                }
            }
            return result;
        }
        public List<OwnLocations> GetOwnedLocation()
        {
            List<OwnLocations> locations = null;
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                locations = connection.Table<OwnLocations>().ToList();
            }
            return locations;
        }
    }
}
