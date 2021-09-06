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
            UpdateWarehuseList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string warehouseName = wareHouseNameTextBox.Text;
            if (!string.IsNullOrEmpty(warehouseName))
            {
                string message = string.Empty;
                ClassLibrary.OwnLocations ownLocations = new ClassLibrary.OwnLocations() { location = warehouseName };
                dataBaseManager.AddOwnedLocation(ownLocations, ref message);
                int index = outputListView.Items.Count + 1;
                ClassLibrary.ListViewDisplayContent displayContent = new ClassLibrary.ListViewDisplayContent(index, warehouseName, message);
                outputListView.Items.Add(displayContent);
                wareHouseNameTextBox.Text = string.Empty;
                UpdateWarehuseList();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string warehouseName = wareHouseNameTextBox.Text;
            if (!string.IsNullOrEmpty(warehouseName))
            {
                string message = string.Empty;
                dataBaseManager.DeleteOwnedLocation(warehouseName, ref message);
                int index = outputListView.Items.Count + 1;
                ClassLibrary.ListViewDisplayContent displayContent = new ClassLibrary.ListViewDisplayContent(index, warehouseName, message);
                outputListView.Items.Add(displayContent);
                UpdateWarehuseList();
            }
        }

        public void UpdateWarehuseList()
        {
            if (dataBaseManager != null)
            {
                warehouseListView.Items.Clear();
                List<ClassLibrary.OwnLocations> ownLocations = dataBaseManager.OwnLocationList;
                for (int i = 0; i < ownLocations.Count; i++)
                {
                    int index = i + 1;
                    string name = ownLocations[i].location;
                    ClassLibrary.ListViewDisplayContent content = new ClassLibrary.ListViewDisplayContent(index, name);
                    warehouseListView.Items.Add(content);
                }
            }
        }
    }
}
