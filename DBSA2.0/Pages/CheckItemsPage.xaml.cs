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
    /// Interaction logic for CheckItemsPage.xaml
    /// </summary>
    public partial class CheckItemsPage : Page
    {
        ClassLibrary.DataBaseManager dataBaseManager;
        public CheckItemsPage(ClassLibrary.DataBaseManager dataBaseManager)
        {
            InitializeComponent();
            this.dataBaseManager = dataBaseManager;
        }
        private void CheckItemStatusClick(object sender, RoutedEventArgs e)
        {
            string barcode = textBoxSMCID.Text;
            int itemSelectedIndex = itemNameListView.SelectedIndex;
            if (!string.IsNullOrEmpty(barcode)
                && !string.IsNullOrWhiteSpace(barcode)
                && itemSelectedIndex >= 0)
            {

                ClassLibrary.ListViewDisplayContent selectedItem = (ClassLibrary.ListViewDisplayContent)itemNameListView.SelectedItem;
                string itemSelectedName = selectedItem.name;
                string message = string.Empty;
                ClassLibrary.ItemData itemData = dataBaseManager.GetItemData(barcode, itemSelectedName, ref message);
                int index = dataListView.Items.Count + 1;
                ClassLibrary.ListViewDisplayContent listViewDisplayContent;
                if (itemData != null)
                {
                    message = message + string.Format(":Lokasi{0} Waktu terdaftar{1}", itemData.location, itemData.time);
                    listViewDisplayContent = new ClassLibrary.ListViewDisplayContent(index, barcode, message);
                }
                else
                {
                    listViewDisplayContent = new ClassLibrary.ListViewDisplayContent(index, barcode, message);
                }
                dataListView.Items.Add(listViewDisplayContent);
            }
        }
        public void UpdateUI()
        {
            itemNameListView.Items.Clear();
            List<ClassLibrary.Item> itemNameList = dataBaseManager.ItemList;

            for (int i = 0; i < itemNameList.Count; i++)
            {
                int index = i + 1;
                ClassLibrary.ListViewDisplayContent displayContent = new ClassLibrary.ListViewDisplayContent(index, itemNameList[i].itemName);
                itemNameListView.Items.Add(displayContent);
            }
        }
    }

}
