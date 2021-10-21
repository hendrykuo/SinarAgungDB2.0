using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSA2._0.ClassLibrary
{
    public static class Helper
    {
        static public List<ClassLibrary.Customer> SortNamesAlphabetically(List<ClassLibrary.Customer> customers)
        {
            List<ClassLibrary.Customer> sorted = new List<Customer>();
            if (customers.Count > 0)
            {
                List<string> temporaryList = new List<string>();
                foreach (var cust in customers)
                {
                    temporaryList.Add(cust.name);
                }
                temporaryList.Sort();
                foreach (var cust in temporaryList)
                {
                    Customer newCust = new Customer();
                    newCust.name = cust;
                    sorted.Add(newCust);
                }
                return sorted;
            }
            else
            {
                return null;
            }
        }
        static public List<string> SortItemNameAlphabetically(List<ClassLibrary.Item> items)
        {
            List<Item> sorted = new List<Item>();
            if (items.Count > 0)
            {
                List<string> temp = new List<string>();
                foreach (var item in items)
                {
                    temp.Add(item.itemName);
                }
                temp.Sort();
                return temp;
            }
            else
            {
                return null;
            }
        }
        
    }
}
