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
            List<string> sortedItem = ClassLibrary.Helper.SortItemNameAlphabetically(items);
            if (sortedItem != null)
            {
                for (int i = 0; i < sortedItem.Count; i++)
                {
                    ClassLibrary.ListViewDisplayContent content = new ClassLibrary.ListViewDisplayContent(i+1 , sortedItem[i]);
                    itemNameListBox.Items.Add(content);
                }
            }
            else
            {
                int index = 1;
                ClassLibrary.ListViewDisplayContent displayContent = new ClassLibrary.ListViewDisplayContent(index, "Kosong");
                itemNameListBox.Items.Add(displayContent);
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
            //Rules:
            //1. location must be selected
            //2. item type must be selected
            //3. check barcode length
            SaveData();
        }

        private void TextBoxSMCID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveData();
            }
        }
        private void SaveData()
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
                dataBaseManager.SaveNewItemData(barcode, selectedItem.name, selectedLocation.name, ref message);
                if (!message.Contains("Gagal"))
                {
                    Console.Beep();
                }
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
            int index = savedDataListView.Items.Count + 1;

            ClassLibrary.ListViewDisplayContent content = new ClassLibrary.ListViewDisplayContent(index, textBoxSMCID.Text, message);
            savedDataListView.Items.Add(content);
            itemNameListBox.SelectedItem = -1;
            itemLocationListBox.SelectedItem = -1;
            textBoxSMCID.Text = string.Empty;
        }
    }
}
