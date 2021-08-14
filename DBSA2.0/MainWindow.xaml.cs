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

        class ButtonData
        {
            public SolidColorBrush foreground;
            public SolidColorBrush background;
            public SolidColorBrush borderBrush;
        }
        Button[] buttons;
        ButtonData[] buttonOriginalState;
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
            buttonOriginalState = new ButtonData[numOfButton];
            for (int i = 0; i < numOfButton; i++)
            {
                buttonOriginalState[i] = new ButtonData() { background = buttons[0].Background.c}
            }
        }
        private void ResetButtonColor()
        { 
        
        }
        private void inputItemButtonClicked(object sender, RoutedEventArgs e)
        {
            programMainFrame.Content = new Temporary("Input Item Page");
        }

        private void registerItemButtonClicked(object sender, RoutedEventArgs e)
        {
            programMainFrame.Content = new Temporary("Register Item Page");
        }
    }
}
