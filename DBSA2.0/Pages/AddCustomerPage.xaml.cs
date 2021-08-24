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
    /// Interaction logic for AddCustomerPage.xaml
    /// </summary>
    public partial class AddCustomerPage : Page
    {
        const double listViewAspectRatio = 1.0d / 5.0d;
        public AddCustomerPage()
        {
            InitializeComponent();
        
        }
        private void SettupListViewColumnWidth()
        {
            double listViewWidth = listView.ActualWidth;
            listViewNoColumn.Width = listViewWidth * listViewAspectRatio;
            listViewNameColumn.Width = listViewWidth * 4 * listViewAspectRatio;
        }
        public void Reset()
        {
            //use this to force calculate the window
            this.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            this.Arrange(new Rect(0, 0, this.DesiredSize.Width, this.DesiredSize.Height));
            SettupListViewColumnWidth();
        }
    }
}
