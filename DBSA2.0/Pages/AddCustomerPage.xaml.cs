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
        ClassLibrary.DataBaseManager dataBaseManager = null;
        const double listViewAspectRatio = 1.0d / 5.0d;
        public AddCustomerPage(ClassLibrary.DataBaseManager dataBaseManager)
        {
            InitializeComponent();
            this.dataBaseManager = dataBaseManager;
            UpdateSavedCustomerListView();
            //dataBaseManager.GetCustomerList();
        }
        //private void SettupListViewColumnWidth()
        //{
        //    double listViewWidth = outputListView.ActualWidth;
        //    listViewNoColumn.Width = listViewWidth * listViewAspectRatio;
        //    listViewNameColumn.Width = listViewWidth * 4 * listViewAspectRatio;
        //}
        //public void Reset()
        //{
        //    //use this to force calculate the window
        //    this.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        //    this.Arrange(new Rect(0, 0, this.DesiredSize.Width, this.DesiredSize.Height));
        //    SettupListViewColumnWidth();
        //}
        private void UpdateSavedCustomerListView()
        {
            List<ClassLibrary.Customer> customers = dataBaseManager.CustomersList;
            savedCustomerListView.Items.Clear();
            for (int i = 0; i < customers.Count; i++)
            {
                ClassLibrary.ListViewDisplayContent content = new ClassLibrary.ListViewDisplayContent(i+1, customers[i].name);
                savedCustomerListView.Items.Add(content);
            }
        }
        private void saveButtonClick(object sender, RoutedEventArgs e)
        {
            string customerName = customerNameTextBox.Text;
            if (!string.IsNullOrEmpty(customerName))
            {
                ClassLibrary.Customer customer = new ClassLibrary.Customer() { name = customerName };
                string errorMessage = string.Empty;
                dataBaseManager.AddCustomer(customer, ref errorMessage);
                int index = outputListView.Items.Count + 1;
                ClassLibrary.ListViewDisplayContent customerUI = new ClassLibrary.ListViewDisplayContent(index, customerName, errorMessage);
                outputListView.Items.Add(customerUI);
                UpdateSavedCustomerListView();
            }
        }

        private void deleteButtonClick(object sender, RoutedEventArgs e)
        {
            string customerName = customerNameTextBox.Text;
            if (!string.IsNullOrEmpty(customerName))
            {
                string message = string.Empty;
                dataBaseManager.DeleteCustomer(customerName, ref message);
                ClassLibrary.ListViewDisplayContent listViewDisplayContent = new ClassLibrary.ListViewDisplayContent(outputListView.Items.Count+1, customerName, message);
                outputListView.Items.Add(listViewDisplayContent);
                UpdateSavedCustomerListView();
            }
        }
    }
}
