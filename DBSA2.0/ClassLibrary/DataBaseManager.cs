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
                for (int i = 0; i < items.Count; i++)
                {
                    SQLiteCommand command;
                    string commandString = CreateTableIfNotExistString(items[i]);
                    command = connection.CreateCommand(commandString);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void CreateItemTable(Item item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                string commandString = CreateTableIfNotExistString(item);
                SQLiteCommand command = connection.CreateCommand(commandString);
                command.ExecuteNonQuery();
            }
        }
        public bool UpdateItemData(string barcode, string name, string location, ref string message)
        {
            Item selected = GetItem(name);
            bool isSucces = false;
            if (selected != null)
            {
                if (barcode.Length == selected.characterLength)
                {
                    if (IsBarcodeExist(barcode, name))
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
                        message = string.Format("Barcode Belum Di Daftarkan");
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
        public bool SaveNewItemData(string barcode, string name, string location, ref string message)
         {
            bool isSucces = false;
            Item selected = GetItem(name);
            if (selected != null)
            {
                if (barcode.Length == selected.characterLength)
                {
                    if (!IsBarcodeExist(barcode, name))
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
                        message = string.Format("Barcode sudah tersimpan");
                    }
                }
                else
                {
                    message = string.Format("Gagal: panjang id input: {0} panjang id yang di perlukan: {1}", barcode.Length, selected.characterLength);
                }
            }
            else
            {
                message = string.Format("Gagal: Tidak di temukan di database");
            }
            return isSucces;
        }
        public bool SaveItemData(string itemName, ItemData itemData, ref string message)
        {
            Item selected = GetItem(itemName);
            bool isSucces = false;
            if (selected != null)
            {
                if (itemData.barcode.Length == selected.characterLength)
                {
                    if (!IsBarcodeExist(itemData.barcode, itemName))
                    {
                        using (SQLiteConnection connection = new SQLiteConnection(dbName))
                        {

                            SQLiteCommand command;
                            string commandString = InsertOrReplaceIntoString(itemName, itemData);
                            command = connection.CreateCommand(commandString);
                            command.ExecuteNonQuery();
                            message = "Sukses";
                            isSucces = true;
                        }
                    }
                    else
                    {
                        message = string.Format("Barcode sudah tersimpan");
                    }
                }
                else
                {
                    message = string.Format("Error: panjang id input: {0} panjang id yang di perlukan: {1}", itemData.barcode.Length, selected.characterLength);
                }
            }
            else
            {
                message = string.Format("Error: Tidak di temukan di database");
            }
            return isSucces;
        }
        private bool IsBarcodeExist(string barcode, string itemName)
        {
            bool exist = false;
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                try
                {
                    List<ItemData> items = connection.Query<ItemData>(GetItemDataString(barcode, itemName));
                    //connection.Table();
                    if (items.Count > 0)
                    {
                        if (items[0].barcode != null)
                        {
                            if (items[0].barcode == barcode)
                            {
                                exist = true;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    //TODO: LOGGING
                    //throw;
                }
            }
            return exist;
        }
        public ItemData GetItemData(string barcode, string itemName, ref string message)
        {
            ItemData itemData = null;
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                try
                {
                    List<ItemData> items = connection.Query<ItemData>(GetItemDataString(barcode, itemName));
                    if (items.Count > 0)
                    {
                        if (items[0] != null)
                        {
                            itemData = items[0];
                            message = "Succes";
                        }
                    }
                    else
                    {
                        message = "Belum terdaftar";
                    }
                }
                catch (Exception e)
                {
                    message = "Gagal:" + e.Message;
                }
            }
            return itemData;
        }
        //Barcode Stuff
        private string GetItemDataString(string barcode, string itemName)
        {
            string commandString = string.Format("SELECT * FROM '{0}' WHERE barcode='{1}'", itemName, barcode);
            return commandString;
        }
        private string CheckIfBarcodeExistString(string barcode, string itemName)
        {
            string commandString = string.Format("SELECT EXISTS(SELECT barcode FROM '{0}' WHERE barcode='{1}');", itemName, barcode);
            return commandString;
        }
        private string CreateTableIfNotExistString(Item item)
        {
            string result = string.Format("CREATE TABLE IF NOT EXISTS '{0}'(barcode TEXT PRIMARY KEY NOT NULL, time TEXT NOT NULL, location TEXT NOT NULL);", item.itemName);
            return result;
        }
        private string InsertOrReplaceIntoString(string itemName, ItemData data)
        {
            string barcode = data.barcode;
            string time = data.time;
            string location = data.location;
            if (data.barcode.Contains('\''))
            {
                barcode = UpdateAposthrope(barcode);
            }
            if (data.time.Contains('\''))
            {
                time = UpdateAposthrope(time);
            }
            if (data.location.Contains('\''))
            {
                location = UpdateAposthrope(location);
            }

            string result = string.Format("INSERT OR REPLACE INTO '{0}'(barcode, time, location) VALUES('{1}', '{2}', '{3}')", itemName, barcode, time, location);
            return result;
        }
        private string UpdateAposthrope(string data)
        {
            int count = data.Length;
            string newData = string.Empty;
            char aphostrope = '\'';
            for (int i = 0; i < count; i++)
            {
                char c = data[i];
                if (c == aphostrope)
                {
                    
                    newData += "''";
                }
                else
                { 
                    newData += data[i];
                }
            }
            return newData;
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
        public int GetItemBarcodeCharacterLength(string itemName)
        {
            int result = int.MinValue;
            items = ItemList;
            for (int i = 0; i < items.Count; i++)
            {
                if (itemName == items[i].itemName)
                {
                    result = items[i].characterLength;
                    break;
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
            if (IsItemExist(itemName))
            {
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
            }
            else
            {
                message = string.Format("Gagal: {0} Tidak Di Temukan", itemName);
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
                    message = "Sukses di hapus";
                }
                catch (SQLite.SQLiteException e)
                {
                    message = "Gagal di hapus : " + e.Message;
                }
                catch (Exception e)
                {
                    message = "Gagal di hapus : " + e.Message;
                }
            }
        }

        //customers
        public int AddCustomer(Customer customer, ref string message)
        {
            int result = int.MinValue;
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
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
        protected bool IsItemExist(string itemName)
        {
            items = ItemList;
            //Item selected = null;
            bool isExist = false;
            foreach (var item in items)
            {
                if (item.itemName == itemName)
                {
                    //selected = item;
                    isExist = true;
                    break;
                }
            }
            return isExist;
        }
        protected Item GetItem(string itemName)
        {
            Item item = null;
            items = ItemList;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].itemName == itemName)
                {
                    item = items[i];
                    break;
                }
            }
            return item;
        }
    }
}
