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
                connection.CreateTable();
                connection.CreateTable<ClassLibrary.OwnLocations>();
                connection.CreateTable<ClassLibrary.Customer>();
            }
            InitialLocationSettup();
            ReorderOwnedLocationIndex();
        }
        public void InitialLocationSettup()
        {
            AddNewOwnedLocation(new OwnLocations() { location = "Gudang 65"});
            AddNewOwnedLocation(new OwnLocations() { location = "Gudang Margo"}) ;
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
                catch (SQLite.SQLiteException e)
                {
                    string exceptionMsg = e.Message.ToString();
                    if (exceptionMsg.Contains("UNIQUE constraint failed"))
                    {
                        //to do....
                        //handle this
                    }
                    //{ "UNIQUE constraint failed: OwnLocations.location"}
                }
                catch (Exception e)
                {
                    string exceptionMsg = e.Message;
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
        public void ReorderOwnedLocationIndex()
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                List<OwnLocations> locs= connection.Table<OwnLocations>().ToList();
                for (int i = 0; i < locs.Count; i++)
                {
                    locs[i].index = (uint)i + 1;
                    connection.InsertOrReplace(locs[i]);
                }
            }
        }

        public List<CustomerUI> GetCustomerList()
        {
            List<CustomerUI> customerUIs = null;
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                
            }
            return customerUIs;
        }
    }
}
