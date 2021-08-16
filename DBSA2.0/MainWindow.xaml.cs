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

namespace DBSA2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button disabledButton = null;
        Button[] buttons;
        public MainWindow()
        {
            InitializeComponent();
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
            programMainFrame.Content = new Pages.InpuItemPage();
            //programMainFrame.Content = new Temporary("Input Item Page");
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
    }
}
