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
using IronXL;

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
            string message = string.Empty;
            if (string.IsNullOrWhiteSpace(path))
            {
                message = "Lokasi file belum di input";
            }
            else
            {
                if (saveTypeComboBox.SelectedItem == null)
                {
                    message = "Tipe data belum di pilih";
                }
                else
                { 
                    bool check = IsFileExist(path);
                    if (check)
                    {
                        int index = saveTypeComboBox.SelectedIndex;
                        
                        string selectedType = saveTypeComboBox.Items[index].ToString();
                        message = "File exist";
                        ReadExcel(path, selectedType);
                    }
                    else
                    {
                        message = "File tidak di temukan"; ;
                    }
                
                }
            
            }

            AddItemToListView(dataListView, path, message);
        }
        void AddItemToListView(ListView listView, string name, string message)
        {
            int index = listView.Items.Count + 1;
            ClassLibrary.ListViewDisplayContent displayContent = new ClassLibrary.ListViewDisplayContent(index, name, message);
            listView.Items.Add(displayContent);
        }
        void ReadExcel(string path, string saveType)
        {
            try
            {
                WorkBook workBook = WorkBook.Load(path);
                WorkSheet sheet = workBook.WorkSheets.First();
                int columnCount = sheet.ColumnCount;
                int rowCount = sheet.RowCount;
                if (saveType.Contains("Lokasi Gudang"))
                {
                    //Not implemented
                }
                else if (saveType.Contains("Daftar Barang"))
                {
                    for (int i = 1; i < rowCount; i++)
                    {
                        string message = string.Empty;
                        ClassLibrary.Item item = new ClassLibrary.Item();

                        item.itemName = sheet.GetRow(i).Columns[1].ToString();
                        item.characterLength = int.Parse(sheet.GetRow(i).Columns[2].ToString());
                        dataBaseManager.AddItem(item, ref message);
                        if (message.Contains("sukses"))
                        {
                            dataBaseManager.CreateItemTable(item);
                            message = string.Format("{0} Tersimpan", item.itemName);
                        }
                        AddItemToListView(dataListView, item.itemName, message);
                    }
                }
                else if ( saveType.Contains("Daftar Customer"))
                {
                    for (int i = 1; i < rowCount; i++)
                    {
                        ClassLibrary.Customer customer = new ClassLibrary.Customer();
                        customer.name = sheet.GetRow(i).Columns[1].ToString();
                        string message = string.Empty;
                        dataBaseManager.AddCustomer(customer, ref message);
                        if (message.Contains("Sukses"))
                        {
                            message = string.Format("{0} Tersimpan", customer.name);
                        }
                        AddItemToListView(dataListView, customer.name, message);
                    }
                }
                else
                { 
                    //TODO: NOT FOUND
                }
            }
            catch (Exception e)
            { 
            
            }
        }
        private void SaveItemData(object sender, RoutedEventArgs e)
        {
            string path = itemFilePath.Text;
            string message = string.Empty;
            if (string.IsNullOrWhiteSpace(path))
            {
                message = "Lokasi file belum di input";
            }
            else
            {
                if (itemNameComboBox.SelectedItem == null)
                {
                    message = "Nama Barang belum di pilih";
                }
                else
                {
                    bool check = IsFileExist(path);
                    if (check)
                    {
                        int index = itemNameComboBox.SelectedIndex;

                        string selectedType = itemNameComboBox.Items[index].ToString();
                        message = "File exist";
                        SaveItemData(path, selectedType);
                    }
                    else
                    {
                        message = "File tidak di temukan"; ;
                    }

                }

            }
            AddItemToListView(dataListView, path, message);
        }
        private void SaveItemData(string path, string itemName)
        {
            try
            {
                string message = string.Empty;
                WorkBook workBook = WorkBook.Load(path);
                WorkSheet sheet = workBook.WorkSheets.First();
                //int columnCount = sheet.ColumnCount;
                int rowCount = sheet.RowCount;
                //int rowCount = 4917;
                //input 
                for (int i = 0; i < rowCount; i++)
                {
                    ClassLibrary.ItemData itemData = new ClassLibrary.ItemData();
                    itemData.barcode    = sheet.GetRow(i).Columns[0].ToString();
                    itemData.location   = sheet.GetRow(i).Columns[1].ToString();
                    itemData.time       = sheet.GetRow(i).Columns[2].ToString();
                    dataBaseManager.SaveItemData(itemName,itemData, ref message);
                    AddItemToListView(dataListView, itemData.barcode, message);
                }
            }
            catch (Exception e)
            {
                AddItemToListView(dataListView, path, e.Message);
                // throw;
            }
        }
    }
}
