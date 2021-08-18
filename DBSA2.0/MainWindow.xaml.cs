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
using System.ComponentModel;
namespace DBSA2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ClassLibrary.Location> warehouseLocation;

        Pages.InputItemPage inputItemPage;
        Button disabledButton = null;
        Button[] buttons;
        public MainWindow()
        {
            InitializeComponent();
            
            SettupPages();
            
        }
        private void SettupPages()
        {
            warehouseLocation = new List<ClassLibrary.Location>();
            warehouseLocation.Add(new ClassLibrary.Location(0, "Gudang Margo"));
            warehouseLocation.Add(new ClassLibrary.Location(1, "Gudang 65"));
            inputItemPage = new Pages.InputItemPage(warehouseLocation);
        }
        private void SettupButtonData()
        {
            int numOfButton = 4;
            buttons = new Button[numOfButton];
            buttons[0] = inputItemBtn;
            buttons[1] = registerItemBtn;
            buttons[2] = checkItemBtn;
            buttons[3] = utilityBtn;
        }
        void ToggleButton(Button button)
        {
            if (disabledButton != null)
            {
                disabledButton.IsEnabled = true;
            }
            button.IsEnabled = false;
            disabledButton = button;
        }
        private void inputItemButtonClicked(object sender, RoutedEventArgs e)
        {
            ToggleButton(sender as Button);
            programMainFrame.Content = inputItemPage;
        }

        private void registerItemButtonClicked(object sender, RoutedEventArgs e)
        {
            ToggleButton(sender as Button);
            programMainFrame.Content = new Temporary("Register Item Page");
        }

        private void checkItemButtonClicked(object sender, RoutedEventArgs e)
        {
            ToggleButton(sender as Button);
            programMainFrame.Content = new Temporary("Check Item Item Page");
        }

        private void utilityButtonClicked(object sender, RoutedEventArgs e)
        {
            ToggleButton(sender as Button);
            programMainFrame.Content = new Temporary("Utility Item Page");
        }


        private void testBtnClicked(object sender, RoutedEventArgs e)
        {
            uint test = (uint)warehouseLocation.Count - 1;
            ClassLibrary.Location newLocation = new ClassLibrary.Location(test, "Temporary" + (warehouseLocation.Count-1).ToString());
            warehouseLocation.Add(newLocation);
            inputItemPage.UpdateWarehouseLocations();
        }
    }
}
