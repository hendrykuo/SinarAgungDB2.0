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
        List<ClassLibrary.Location> warehouseLocations;
        public InputItemPage(List<ClassLibrary.Location> warehouseLocations)
        {
            InitializeComponent();
            this.warehouseLocations = warehouseLocations;
            itemLocationListBox.Items.Add(warehouseLocations[0]);
            itemLocationListBox.Items.Add(warehouseLocations[1]);
        }
        public void UpdateWarehouseLocations()
        {

            int locationsDisplayed = itemLocationListBox.Items.Count;
            int locationInDataBase = warehouseLocations.Count;
            if (locationsDisplayed < locationInDataBase)
            {
                ClassLibrary.Location newLocation = warehouseLocations[locationsDisplayed];
                itemLocationListBox.Items.Add(newLocation);
            }
        }
    }
}
