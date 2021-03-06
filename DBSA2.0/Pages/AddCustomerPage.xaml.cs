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
        public void UpdateUI()
        {
            UpdateSavedCustomerListView();
        }
        private void UpdateSavedCustomerListView()
        {
            List<ClassLibrary.Customer> customers = dataBaseManager.CustomersList;
            List<ClassLibrary.Customer> sortedCustomers = ClassLibrary.Helper.SortNamesAlphabetically(customers);
            savedCustomerListView.Items.Clear();
            if (sortedCustomers != null)
            {
                for (int i = 0; i < sortedCustomers.Count; i++)
                {
                    ClassLibrary.ListViewDisplayContent content = new ClassLibrary.ListViewDisplayContent(i + 1, sortedCustomers[i].name);
                    savedCustomerListView.Items.Add(content);
                }
            }
            else
            {
                int index = 1;
                ClassLibrary.ListViewDisplayContent content = new ClassLibrary.ListViewDisplayContent(index, "Kosong");
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
            customerNameTextBox.Text = string.Empty;
        }

        private void deleteButtonClick(object sender, RoutedEventArgs e)
        {
            string data = customerNameTextBox.Text;
            int selectedIndex = savedCustomerListView.SelectedIndex;
            string customerName = string.Empty;
            if (!string.IsNullOrEmpty(data))
            {
                customerName = data;
            }
            else if (savedCustomerListView.SelectedIndex >= 0 )
            { 
                ClassLibrary.ListViewDisplayContent displayContent = (ClassLibrary.ListViewDisplayContent)savedCustomerListView.Items[selectedIndex];
                customerName = displayContent.name;
            }
            
            if (!string.IsNullOrEmpty(customerName))
            {
                string message = string.Empty;
                dataBaseManager.DeleteCustomer(customerName, ref message);
                ClassLibrary.ListViewDisplayContent listViewDisplayContent = new ClassLibrary.ListViewDisplayContent(outputListView.Items.Count + 1, customerName, message);
                outputListView.Items.Add(listViewDisplayContent);
                UpdateSavedCustomerListView();
            }
            customerNameTextBox.Text = string.Empty;
            savedCustomerListView.SelectedIndex = -1;
        }
    }
}
