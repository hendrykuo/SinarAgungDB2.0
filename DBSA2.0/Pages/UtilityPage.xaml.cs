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
using System.IO;
using ExcelDataReader;

namespace DBSA2._0.Pages
{
    /// <summary>
    /// Interaction logic for UtilityPage.xaml
    /// </summary>
    public partial class UtilityPage : Page
    {
        ClassLibrary.DataBaseManager dataBaseManager;
        public UtilityPage(ClassLibrary.DataBaseManager dataBaseManager)
        {
            InitializeComponent();
            this.dataBaseManager = dataBaseManager;
            UpdateItemList();
        }
        public void UpdateUI()
        {
            UpdateItemList();
            dataListView.Items.Clear();
            saveTypeComboBox.SelectedIndex = 0;
            itemNameComboBox.SelectedIndex = 0;
        }
        private void UpdateItemList()
        {
            itemNameComboBox.Items.Clear();
            List<ClassLibrary.Item> items = dataBaseManager.ItemList;
            foreach (ClassLibrary.Item item in items)
            {
                itemNameComboBox.Items.Add(item.itemName);
            }
        }
        bool IsFileExist(string filePath)
        {
            bool isValid = false;
            if (!string.IsNullOrWhiteSpace(filePath))
            {
                //check if path valid
                isValid = File.Exists(filePath);
            }
            return isValid;
        }

        private void SaveCommonData(object sender, RoutedEventArgs e)
        {
            string path = @filePathForCommonFile.Text;
            
            bool check = IsFileExist(path);
            string message = "File not found";
            if (check)
            {
                message = "File exist";
                ReadExcel(path);
            }
            AddItemToListView(dataListView, path, message);
        }
        void AddItemToListView(ListView listView, string name, string message)
        {
            int index = listView.Items.Count + 1;
            ClassLibrary.ListViewDisplayContent displayContent = new ClassLibrary.ListViewDisplayContent(index, name, message);
            listView.Items.Add(displayContent);
        }
        void ReadExcel(string path)
        {
            using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                try
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            while (reader.Read())
                            {
                                //reader.GetString();
                            }
                        } while (reader.NextResult());
                        //System.Data.DataSet dataSet = reader.AsDataSet();
                    
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
