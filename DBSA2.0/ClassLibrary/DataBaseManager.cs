using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
//using SQLitePCL;
namespace DBSA2._0.ClassLibrary
{
    public class DataBaseManager
    {
        const string dbName = "programDB.db";
        //local stuff
        List<Item> items = new List<Item>();
        public List<Item> ItemListView
        {
            get
            {
                UpdateItem();
                return items;
            }
            
        }
        List<OwnLocations> ownLocations = new List<OwnLocations>();
        public List<OwnLocations> OwnLocationList
        {
            get
            {
                UpdateOwnedLocation();
                return ownLocations;
            }
        }
        public DataBaseManager()
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                
                connection.CreateTable<ClassLibrary.OwnLocations>();
                connection.CreateTable<ClassLibrary.Customer>();
                connection.CreateTable<ClassLibrary.Item>();
                UpdateItem();
            }
        }

        //item
        public int AddItem(Item item, ref string message)
        {
            int result = int.MinValue;
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                try
                {
                    result = connection.Insert(item);
                    message = "sukses";
                    UpdateItem();
                }
                catch (SQLite.SQLiteException e)
                {
                    message = "Gagal: " + e.Message;
                }
                catch (Exception e)
                {
                    message = "Gagal: " + e.Message;
                }
            }
            return result;
        }
        public int DeleteItem(Item item, ref string message)
        {
            int result = int.MinValue;
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                try
                {
                    result = connection.Delete<Item>(item);
                    message = "Sukses";
                    UpdateItem();
                }
                catch (SQLiteException e)
                {
                    message = "Gagal: " + e.Message;
                }
                catch (Exception e)
                {
                    message = "Gagal: " + e.Message;
                }
            }
            return result;
        }
        public int DeleteItem(string itemName, ref string message)
        {
            int result = int.MinValue;
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                try
                {
                    result = connection.Delete<Item>(itemName);
                    message = "Sukses";
                }
                catch (SQLiteException e)
                {
                    message = "Gagal: " + e.Message;
                }
                catch (Exception e)
                {
                    message = "Gagal: " + e.Message;
                }
            }
            return result;
        }
        protected void UpdateItem()
        {
            items.Clear();
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                items = connection.Table<Item>().ToList();
            }
        }
        
        //location
        public int AddOwnedLocation(OwnLocations location, ref string message)
        {
            int result = int.MinValue;
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                
                try
                {
                    result = connection.Insert(location);
                    message = "Sukses";
                }
                catch (SQLite.SQLiteException e)
                {
                    string exceptionMsg = e.Message.ToString();
                    if (exceptionMsg.Contains("UNIQUE constraint failed"))
                    {
                        //to do....
                        //handle this
                    }
                    else
                    {
                    }
                    message = "Gagal : " + e.Message;

                    //{ "UNIQUE constraint failed: OwnLocations.location"}
                }
                catch (Exception e)
                {
                    message = "Gagal : " + e.Message;
                }
            }
            return result;
        }
        protected void UpdateOwnedLocation()
        {
            ownLocations.Clear();
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                ownLocations = connection.Table<OwnLocations>().ToList();
            }
        }
        
        public void DeleteOwnedLocation(string warehouseName, ref string message)
        {
            message = string.Empty;
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                try
                {
                    connection.Delete<OwnLocations>(warehouseName);
                    message = "Sukses";
                }
                catch (SQLite.SQLiteException e)
                {
                    message = "Gagal : " + e.Message;
                }
                catch (Exception e)
                {
                    message = "Gagal : " + e.Message;
                }
            }
        }
        public int AddCustomer(Customer customer, ref string message)
        {
            int result = int.MinValue;
            using(SQLiteConnection connection = new SQLiteConnection(dbName))
            {

                try
                {
                    result = connection.Insert(customer);
                    message = "Sukses";
                }
                catch (SQLite.SQLiteException e)
                {
                    string exceptionMsg = e.Message.ToString();
                    if (exceptionMsg.Contains("UNIQUE constraint failed"))
                    {
                        //to do....
                        //handle this
                    }
                    else
                    {
                    }
                    message = "Error:" + e.Message;
                    //{ "UNIQUE constraint failed: OwnLocations.location"}
                }
                catch (Exception e)
                {
                    message = "Error:" + e.Message;
                }
            }
            return result;
        }
        
        public void DeleteCustomer(string customerName, ref string message)
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                try
                {
                    connection.Delete<Customer>(customerName);
                    message = "Sukses";
                }
                catch (Exception e)
                {
                    message = "Gagal: " + e.Message;
                }
            }
        }
    }
}
