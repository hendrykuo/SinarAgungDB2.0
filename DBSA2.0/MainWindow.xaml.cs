﻿using System;
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
        ClassLibrary.DataBaseManager dataBaseManager;

        Pages.InputItemPage inputItemPage;
        Pages.AddCustomerPage addCustomerPage;
        Pages.ItemPage itemPage;
        Button disabledButton = null;
        Button[] buttons;
        public MainWindow()
        {
            InitializeComponent();
            dataBaseManager = new ClassLibrary.DataBaseManager();
            SettupPages();
            
        }
        private void SettupPages()
        {
            inputItemPage = new Pages.InputItemPage(dataBaseManager);
            addCustomerPage = new Pages.AddCustomerPage(dataBaseManager);
            itemPage = new Pages.ItemPage(dataBaseManager);
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
        private void InputItemButtonClicked(object sender, RoutedEventArgs e)
        {
            programMainFrame.Content = inputItemPage;
            inputItemPage.UpdateUI();
        }

        private void RegisterItemButtonClicked(object sender, RoutedEventArgs e)
        {
            //ToggleButton(sender as Button);
            programMainFrame.Content = new Temporary("Register Item Page");
        }

        private void CheckItemButtonClicked(object sender, RoutedEventArgs e)
        {
            //ToggleButton(sender as Button);
            programMainFrame.Content = new Temporary("Check Item Item Page");
        }
        private void AddOwnLocationClicked(object sender, RoutedEventArgs e)
        {
            programMainFrame.Content = new Pages.WareHousePage(dataBaseManager);
        }

        private void AddCustomerBtnClick(object sender, RoutedEventArgs e)
        {
            programMainFrame.Content = addCustomerPage;
            //addCustomerPage.Reset();
            
        }

        private void itemPageButtonClick(object sender, RoutedEventArgs e)
        {
            programMainFrame.Content = itemPage;
        }

        private void testBtnClicked(object sender, RoutedEventArgs e)
        {
            programMainFrame.Content = new Temporary("Check Item Item Page");
        }

        
    }
}
