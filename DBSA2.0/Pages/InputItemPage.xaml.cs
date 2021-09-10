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
    /// Interaction logic for InputItemPage.xaml
    /// </summary>
    public partial class InputItemPage : Page
    {
        ClassLibrary.DataBaseManager dataBaseManager;
        public InputItemPage(ClassLibrary.DataBaseManager databaseManager)
        {
            InitializeComponent();
            this.dataBaseManager = databaseManager;
        }
        public void UpdateUI()
        {
            itemNameListBox.Items.Clear();
            List<ClassLibrary.Item> items = dataBaseManager.ItemList;
            for (int i = 0; i < items.Count; i++)
            {
                ClassLibrary.ListViewDisplayContent content = new ClassLibrary.ListViewDisplayContent(i+1 , items[i].itemName);
                itemNameListBox.Items.Add(content);
            }
            itemLocationListBox.Items.Clear();
            List<ClassLibrary.OwnLocations> ownLocations = dataBaseManager.OwnLocationList;
            for (int i = 0; i < ownLocations.Count; i++)
            {
                ClassLibrary.ListViewDisplayContent content = new ClassLibrary.ListViewDisplayContent(i+1, ownLocations[i].location);
                itemLocationListBox.Items.Add(content);
            }
        }

        private void AddItemIntoTable(object sender, RoutedEventArgs e)
        {

            int selectedLocationIndex = itemLocationListBox.SelectedIndex;
            int selectedItemIndex = itemNameListBox.SelectedIndex;
            string barcode = textBoxSMCID.Text;
            string message = string.Empty;
            if (selectedItemIndex >= 0
                && selectedLocationIndex >= 0
                && barcode.Length >= 0)
            {
                ClassLibrary.ListViewDisplayContent selectedItem = (ClassLibrary.ListViewDisplayContent)itemNameListBox.SelectedItem;
                ClassLibrary.ListViewDisplayContent selectedLocation = (ClassLibrary.ListViewDisplayContent)itemLocationListBox.SelectedItem;
                //int selectedItemExpectedBarcodeLength = dataBaseManager.GetItemBarcodeCharacterLength(selectedItem.name);
                dataBaseManager.SaveItemData(barcode, selectedItem.name, selectedLocation.name, ref message);
            }
            else
            {
                message = "Gagal:";
                if (barcode.Length == 0)
                {
                    message = message + "[Barcode kosong]";
                }
                if (selectedItemIndex == -1)
                {
                    message = message + "[Tidak ada Barang yang di pilih]";
                }
                if (selectedLocationIndex == -1)
                {
                    message = message + "[Tidak ada Lokasi yang di pilih]";
                }
            }
            int index = savedDataListView.Items.Count;

            ClassLibrary.ListViewDisplayContent content = new ClassLibrary.ListViewDisplayContent(index, textBoxSMCID.Text, message);
            savedDataListView.Items.Add(content);
            itemNameListBox.SelectedItem = -1;
            itemLocationListBox.SelectedItem = -1;
            textBoxSMCID.Text = string.Empty;

        }
        //SELECT FROM name, location
        //shove it into your ass
        

    }
}
