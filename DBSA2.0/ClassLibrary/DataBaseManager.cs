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
        public List<Item> ItemList
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
        List<Customer> customers = new List<Customer>();
        public List<Customer> CustomersList
        {
            get
            {
                UpdateCustomer();
                return customers;
            }
        }
        public DataBaseManager()
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {

                connection.CreateTable<ClassLibrary.OwnLocations>();
                connection.CreateTable<ClassLibrary.Customer>();
                connection.CreateTable<ClassLibrary.Item>();
                items = ItemList;
                /*
                 CREATE TABLE IF NOT EXISTS babi(
	name TEXT PRIMARY KEY NOT NULL,
	date Text NOT NULL);
                 */
                for (int i = 0; i < items.Count; i++)
                {
                    SQLiteCommand command;
                    string commandString = CreateTableIfNotExistString(items[i]);
                    command = connection.CreateCommand(commandString);
                    command.ExecuteNonQuery();
                }
                //UpdateItem();
            }
        }
        public bool AddItem(string barcode, string name, string location, ref string message)
        {
            items = ItemList;
            Item selected = null;
            bool isSucces = false;
            foreach (var item in items)
            {
                if (item.itemName == name)
                {
                    selected = item;
                    break;
                }
            }
            if (selected != null)
            {
                if (barcode.Length == selected.characterLength)
                {
                    using (SQLiteConnection connection = new SQLiteConnection(dbName))
                    {
                        SQLiteCommand command;
                        ItemData data = new ItemData();
                        data.barcode = barcode;
                        data.location = location;
                        data.time = DateTime.Now.ToString();
                        string commandString = InsertOrReplaceIntoString(name, data);
                        command = connection.CreateCommand(commandString);
                        command.ExecuteNonQuery();
                        message = "Sukses";
                        isSucces = true;
                    }
                }
                else
                {
                    message = string.Format("Error: panjang id input: {0} panjang id yang di perlukan: {1}", barcode.Length, selected.characterLength);
                }
            }
            else
            {
                message = string.Format("Error: Tidak di temukan di database");
            }
            return isSucces;
            
        }
        //Barcode Stuff
        private string CreateTableIfNotExistString(Item item)
        {
            string result = string.Format("CREATE TABLE IF NOT EXISTS '{0}'(barcode TEXT PRIMARY KEY NOT NULL, time TEXT NOT NULL, location TEXT NOT NULL);", item.itemName);
            return result;
        }
        private string InsertOrReplaceIntoString(string itemName, ItemData data)
        {
            string result = string.Format("INSERT OR REPLACE INTO '{0}'(barcode, time, location) VALUES({1}, {2}, {3})", itemName, data.barcode, data.time, data.location);
            return result;
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
        
        //customers
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

        protected void UpdateCustomer()
        {
            customers.Clear();
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                customers = connection.Table<Customer>().ToList();
            }
        }
    }
}
