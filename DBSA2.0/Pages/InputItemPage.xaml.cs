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
        List<ClassLibrary.OwnLocations> warehouseLocations;
        public InputItemPage(List<ClassLibrary.OwnLocations> warehouseLocations)
        {
            InitializeComponent();
            this.warehouseLocations = warehouseLocations;
            foreach (ClassLibrary.OwnLocations location in warehouseLocations)
            {
                itemLocationListBox.Items.Add(location);
            }
        }
        public void UpdateWarehouseLocations()
        {

            int locationsDisplayed = itemLocationListBox.Items.Count;
            int locationInDataBase = warehouseLocations.Count;
            if (locationsDisplayed < locationInDataBase)
            {
                
                ClassLibrary.OwnLocations newLocation = warehouseLocations[locationsDisplayed];
                itemLocationListBox.Items.Add(newLocation);

            }
        }
    }
}
