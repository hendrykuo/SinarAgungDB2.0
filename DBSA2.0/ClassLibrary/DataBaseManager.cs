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
        SQLiteConnection connection;
        List<ClassLibrary.Location> locations;

        public DataBaseManager()
        {
            connection = new SQLiteConnection("programDB.db");
            locations = new List<Location>();
            connection.CreateTable<ClassLibrary.Location>();
            //InitialLocationSettup();
        }
        public void InitialLocationSettup()
        {
            AddNewLocation(new Location() { index = 1, location = "Gudang Margo", IsOwnedLocation = true }) ;
            AddNewLocation(new Location() { index = 2, location = "Gudang 65", IsOwnedLocation = true });
        }
        public int AddNewLocation(Location location)
        {
            int result = connection.Insert(location);
            return result;
        }
        private List<Location> GetAllLocation()
        {
            List<Location> locations = connection.Table<Location>().ToList();
            return locations;
        }
        public List<Location> GetOwnedLocation()
        {
            List<Location> locations = GetAllLocation();
            List<Location> ownedLocations = new List<Location>();
            foreach (var location in locations)
            {
                if (location.IsOwnedLocation)
                {
                    ownedLocations.Add(location);
                }
            }
            return ownedLocations;
        }
    }
}
