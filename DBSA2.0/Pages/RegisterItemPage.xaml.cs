using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DBSA2._0.Pages
{
    /// <summary>
    /// Interaction logic for RegisterItemPage.xaml
    /// </summary>
    public partial class RegisterItemPage : Page
    {
        ClassLibrary.DataBaseManager dataBaseManager;
        public RegisterItemPage(ClassLibrary.DataBaseManager dataBaseManager)
        {
            InitializeComponent();
            this.dataBaseManager = dataBaseManager;
            UpdateUI();
        }

        private void RegisterItemButtonClick(object sender, RoutedEventArgs e)
        {
            string barcode = textBoxSMCID.Text;
            int selectedItemIndex = itemListView.SelectedIndex;
            int selectedCustomerIndex = customerListView.SelectedIndex;
            if (!string.IsNullOrWhiteSpace(barcode)
                && selectedItemIndex >= 0
                && selectedCustomerIndex >= 0)
            {
                ClassLibrary.ListViewDisplayContent selectedItem     = (ClassLibrary.ListViewDisplayContent)itemListView.SelectedItem;
                ClassLibrary.ListViewDisplayContent selectedCustomer = (ClassLibrary.ListViewDisplayContent)customerListView.SelectedItem;
                string itemName     = selectedItem.name;
                string customerName = selectedCustomer.name;
                string message = string.Empty;
                dataBaseManager.UpdateItemData(barcode, itemName, customerName, ref message);
                int index = dataListView.Items.Count + 1;
                ClassLibrary.ListViewDisplayContent displayContent = new ClassLibrary.ListViewDisplayContent(index, barcode, message);
                dataListView.Items.Add(displayContent);
            }
        }

        public void UpdateUI()
        {
            List<ClassLibrary.Item> items = dataBaseManager.ItemList;
            List<ClassLibrary.Customer> customers = dataBaseManager.CustomersList;

            customerListView.Items.Clear();
            for (int i = 0; i < customers.Count; i++)
            {
                int index = i + 1;
                ClassLibrary.ListViewDisplayContent listViewDisplayContent = new ClassLibrary.ListViewDisplayContent(index, customers[i].name);
                customerListView.Items.Add(listViewDisplayContent);
            }

            itemListView.Items.Clear(); ;
            for (int i = 0; i < items.Count; i++)
            {
                int index = i + 1;
                ClassLibrary.ListViewDisplayContent listViewDisplayContent = new ClassLibrary.ListViewDisplayContent(index, items[i].itemName);
                itemListView.Items.Add(listViewDisplayContent);
            }
        }
    }
}
