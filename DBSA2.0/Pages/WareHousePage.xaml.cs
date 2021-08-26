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
    /// Interaction logic for WareHousePage.xaml
    /// </summary>
    public partial class WareHousePage : Page
    {
        ClassLibrary.DataBaseManager dataBaseManager;
        public WareHousePage(ClassLibrary.DataBaseManager dataBaseManager)
        {
            InitializeComponent();
            this.dataBaseManager = dataBaseManager;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string warehouseName = wareHouseNameTextBox.Text;
            if (!string.IsNullOrEmpty(warehouseName))
            {
                string message = string.Empty;
                ClassLibrary.OwnLocations ownLocations = new ClassLibrary.OwnLocations() { location = warehouseName };
                dataBaseManager.AddOwnedLocation(ownLocations, ref message);
                uint index = (uint)listView.Items.Count + 1;
                ClassLibrary.ListViewDisplayContent displayContent = new ClassLibrary.ListViewDisplayContent(index, warehouseName, message);
                listView.Items.Add(displayContent);
                wareHouseNameTextBox.Text = string.Empty;
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string warehouseName = wareHouseNameTextBox.Text;
            if (!string.IsNullOrEmpty(warehouseName))
            {
                string message = string.Empty;
                dataBaseManager.DeleteOwnedLocation(warehouseName, ref message);
                uint index = (uint)listView.Items.Count + 1;
                ClassLibrary.ListViewDisplayContent displayContent = new ClassLibrary.ListViewDisplayContent(index, warehouseName, message);
                listView.Items.Add(displayContent);
            }
        }
    }
}
