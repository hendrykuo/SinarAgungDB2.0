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
            RegisterItem();
        }

        public void UpdateUI()
        {
            List<ClassLibrary.Customer> customers = dataBaseManager.CustomersList;
            List<ClassLibrary.Customer> sortedCustomers = ClassLibrary.Helper.SortNamesAlphabetically(customers);
            customerListView.Items.Clear();
            if (sortedCustomers != null)
            {
                for (int i = 0; i < sortedCustomers.Count; i++)
                {
                    int index = i + 1;
                    ClassLibrary.ListViewDisplayContent listViewDisplayContent = new ClassLibrary.ListViewDisplayContent(index, sortedCustomers[i].name);
                    customerListView.Items.Add(listViewDisplayContent);
                }
            }
            else
            {
                int index = 1;
                ClassLibrary.ListViewDisplayContent content = new ClassLibrary.ListViewDisplayContent(index, "Kosong");
                customerListView.Items.Add(content);
            }

            List<ClassLibrary.Item> items = dataBaseManager.ItemList;
            List<string> sortedItem = ClassLibrary.Helper.SortItemNameAlphabetically(items);
            itemListView.Items.Clear();
            if (sortedItem != null)
            {
                for (int i = 0; i < sortedItem.Count; i++)
                {
                    int index = i + 1;
                    ClassLibrary.ListViewDisplayContent listViewDisplayContent = new ClassLibrary.ListViewDisplayContent(index, sortedItem[i]);
                    itemListView.Items.Add(listViewDisplayContent);
                }

            }
            else
            {
                int index = 1;
                ClassLibrary.ListViewDisplayContent content = new ClassLibrary.ListViewDisplayContent(index, "Kosong");
                itemListView.Items.Add(content);
            }
        }

        private void TextBoxSMCID_KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RegisterItem();
            }
        }
        private void RegisterItem()
        {
            string barcode = textBoxSMCID.Text;
            int selectedItemIndex = itemListView.SelectedIndex;
            int selectedCustomerIndex = customerListView.SelectedIndex;
            bool isEmptyString = string.IsNullOrWhiteSpace(barcode);
            if (!isEmptyString
                && selectedItemIndex >= 0
                && selectedCustomerIndex >= 0)
            {
                ClassLibrary.ListViewDisplayContent selectedItem = (ClassLibrary.ListViewDisplayContent)itemListView.SelectedItem;
                ClassLibrary.ListViewDisplayContent selectedCustomer = (ClassLibrary.ListViewDisplayContent)customerListView.SelectedItem;
                string itemName = selectedItem.name;
                string customerName = selectedCustomer.name;
                string message = string.Empty;
                dataBaseManager.UpdateItemData(barcode, itemName, customerName, ref message);
                if (message == "Sukses")
                {
                    message = string.Format("{0}-{1}-{2} Sukses terdaftar", itemName, barcode, customerName);
                    Console.Beep();
                }
                AddItemToListView(dataListView, barcode, message);
            }
            else
            {
                string message = "Gagal:";
                if (isEmptyString)
                {
                    message += "[Barcode belum terisi]";
                }
                if (selectedItemIndex < 0)
                {
                    message += "[Jenis Barang Belum terpilih]";
                }
                if (selectedCustomerIndex < 0)
                {
                    message += "[Customer belum terpilih]";
                }
                AddItemToListView(dataListView, barcode, message);
            }
            textBoxSMCID.Text = string.Empty;
        }

        void AddItemToListView(ListView listView, string barcode, string message)
        {
            int index = listView.Items.Count + 1;
            ClassLibrary.ListViewDisplayContent displayContent = new ClassLibrary.ListViewDisplayContent(index, barcode, message);
            listView.Items.Add(displayContent);
        }

    }
}
