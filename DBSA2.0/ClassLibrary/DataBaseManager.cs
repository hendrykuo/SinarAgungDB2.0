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
        public DataBaseManager()
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                connection.CreateTable<ClassLibrary.OwnLocations>();
                connection.CreateTable<ClassLibrary.Customer>();
            }
            //InitialLocationSettup();
            //ReorderOwnedLocationIndex();
        }

        //public void InitialLocationSettup()
        //{
        //    //error message is not handled here because it is only for example
        //    string errMessage = string.Empty;
        //    AddNewOwnedLocation(new OwnLocations() { location = "Gudang 65"}, ref errMessage);
        //    AddNewOwnedLocation(new OwnLocations() { location = "Gudang Margo"}, ref errMessage) ;
        //}

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
        public List<OwnLocations> GetOwnedLocation()
        {
            List<OwnLocations> locations = null;
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                locations = connection.Table<OwnLocations>().ToList();
            }
            return locations;
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
        //public void ReorderOwnedLocationIndex()
        //{
        //    using (SQLiteConnection connection = new SQLiteConnection(dbName))
        //    {
        //        List<OwnLocations> locs = connection.Table<OwnLocations>().ToList();
        //        for (int i = 0; i < locs.Count; i++)
        //        {
        //            locs[i].index = (uint)i + 1;
        //            connection.InsertOrReplace(locs[i]);
        //        }
        //    }
        //}
        //customer stuff
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
        public List<ListViewDisplayContent> GetCustomerList()
        {
            List<ListViewDisplayContent> customerUIs = new List<ListViewDisplayContent>();
            using (SQLiteConnection connection = new SQLiteConnection(dbName))
            {
                List<Customer> customers = connection.Table<Customer>().ToList();
                for (int i = 0; i < customers.Count; i++)
                {
                    ListViewDisplayContent customerUI = new ListViewDisplayContent((uint)i + 1,customers[i].name);
                }
            }
            return customerUIs;
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
