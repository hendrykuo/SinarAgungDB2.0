﻿#pragma checksum "..\..\..\..\Pages\AddCustomerPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "118AD1B8AE3F32DC13B072E841BA36A0E9318A07"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DBSA2._0.Pages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DBSA2._0.Pages {
    
    
    /// <summary>
    /// AddCustomerPage
    /// </summary>
    public partial class AddCustomerPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\..\Pages\AddCustomerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox customerNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Pages\AddCustomerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView outputListView;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\Pages\AddCustomerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn listViewNoColumn;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\Pages\AddCustomerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn listViewNameColumn;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Pages\AddCustomerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn listviewMessageColumn;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\Pages\AddCustomerPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView savedCustomerListView;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DBSA2.0;component/pages/addcustomerpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\AddCustomerPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.customerNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            
            #line 38 "..\..\..\..\Pages\AddCustomerPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.saveButtonClick);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 43 "..\..\..\..\Pages\AddCustomerPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.deleteButtonClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.outputListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 5:
            this.listViewNoColumn = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 6:
            this.listViewNameColumn = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 7:
            this.listviewMessageColumn = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 8:
            this.savedCustomerListView = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

