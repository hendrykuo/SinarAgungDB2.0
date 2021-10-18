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
using System.Text.RegularExpressions;
namespace DBSA2._0.Pages
{
    /// <summary>
    /// Interaction logic for ItemPage.xaml
    /// </summary>
    public partial class ItemPage : Page
    {
        ClassLibrary.DataBaseManager dataBaseManager;
        public ItemPage(ClassLibrary.DataBaseManager dataBaseManager)
        {
            InitializeComponent();
            this.dataBaseManager = dataBaseManager;
            DataObject.AddPastingHandler(idLengthTextBox, PasteHandler);
            UpdateSavedItemListBox();
        }
        public void UpdateUI()
        {
            UpdateSavedItemListBox();
        }
        private void UpdateSavedItemListBox()
        {
            savedItemListBox.Items.Clear();
            List<ClassLibrary.Item> items = dataBaseManager.ItemList;
            for (int i = 0; i < items.Count; i++)
            {
                string message = items[i].characterLength.ToString();
                ClassLibrary.ListViewDisplayContent displayContent = new ClassLibrary.ListViewDisplayContent(i+1, items[i].itemName, message);
                savedItemListBox.Items.Add(displayContent);
            }
        }
        private void saveButtonClick(object sender, RoutedEventArgs e)
        {
            //check if it is empty or not
            string itemName = itemNameTextBox.Text;
            if (itemName.Length > 0
                && idLengthTextBox.Text.Length > 0)
            {
                string message = string.Empty;
                int idLength = int.MinValue;
                if(int.TryParse(idLengthTextBox.Text, out idLength))
                {
                    //save it to db
                    ClassLibrary.Item item = new ClassLibrary.Item() { itemName = itemName, characterLength = idLength };
                    dataBaseManager.AddItem(item, ref message);
                    int index = outputListView.Items.Count + 1;
                    ClassLibrary.ListViewDisplayContent listViewItem = new ClassLibrary.ListViewDisplayContent(index, itemName, message);
                    outputListView.Items.Add(listViewItem);
                    UpdateSavedItemListBox();
                    dataBaseManager.CreateItemTable(item);
                }
            }
            itemNameTextBox.Text = string.Empty;
            idLengthTextBox.Text = string.Empty;
        }

        private void deleteButtonClick(object sender, RoutedEventArgs e)
        {
            string itemName = itemNameTextBox.Text;
            bool isDelete = false;
            if (!string.IsNullOrEmpty(itemName))
            {
                isDelete = true;
            }
            else
            {
                itemName = ((ClassLibrary.ListViewDisplayContent)savedItemListBox.SelectedItem).name;
                isDelete = true;
            }
            if (isDelete)
            { 
                string message = string.Empty;
                dataBaseManager.DeleteItem(itemName, ref message);
                int index = outputListView.Items.Count + 1;
                ClassLibrary.ListViewDisplayContent listViewDisplayContent = new ClassLibrary.ListViewDisplayContent(index, itemName, message);
                outputListView.Items.Add(listViewDisplayContent);
                UpdateSavedItemListBox();
            }
            itemNameTextBox.Text = string.Empty;
            savedItemListBox.SelectedIndex = -1;
        }


        //copied code from
        //https://wpf.2000things.com/tag/previewtextinput/
        private bool IsNumeric(string s)
        {
            Regex r = new Regex(@"^[0-9]*$");

            return r.IsMatch(s);
        }
        private void previewIDLengthTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
            }
        }

        private void previewKeyDownIDLengthTextInput(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void PasteHandler(object sender, DataObjectPastingEventArgs e)
        {
            TextBox tb = sender as TextBox;
            bool textOK = false;

            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                // Allow pasting only alphabetic
                string pasteText = e.DataObject.GetData(typeof(string)) as string;
                if (IsNumeric(pasteText))
                { 
                    textOK = true;
                }
            }

            if (!textOK)
            { 
                e.CancelCommand();
            }
        }
    }
}
