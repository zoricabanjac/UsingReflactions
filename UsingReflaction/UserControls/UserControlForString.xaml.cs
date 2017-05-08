﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using UsingReflaction.TestEntities;

namespace UsingReflaction.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlForString.xaml
    /// </summary>
    public partial class UserControlForString : UserControlBase
    {
        public string ControlName
        {
            get
            {
                return "txb" + NameAbr;
            }
        }

        public UserControlForString()
        {
            InitializeComponent();    
        }

        public UserControlForString(MemberInfo info, StackPanel stackPanelInfo, Customer myCustomer) : this()
        {
            this.MyCustomer = myCustomer;
            this.MemberInformation = info;
            this.StackPanelInfo = stackPanelInfo;

            if(!TypeFullName.Contains("System"))
            {
                btnSetValue.IsEnabled = false;
            }
          
            lblLabel.Text = NameAbr;
        }

        private void btnGetValue_Click(object sender, RoutedEventArgs e)
        {
            // MemberInfo info = MyCustomer.GetType().FindMembers(MemberTypes.Property | MemberTypes.Field, BindingFlags.Public | BindingFlags.Instance, new MemberFilter(DelegateToSearchCriteria), Name).FirstOrDefault();
            // if (info is PropertyInfo)
            if (PropertyInformation != null)
            {
                if (PropertyInformation.PropertyType.FullName.Contains("System"))
                {
                    txbTextBox.Text = PropertyInformation.GetValue(MyCustomer).ToString();
                }
                else
                {
                    txbTextBox.Text = GetPropertyValue(MyCustomer, NameAbr).ToString();
                }
            }
            else if (FieldInformation != null)
            {
                if (FieldInformation.FieldType.FullName.Contains("System"))
                {
                    txbTextBox.Text = FieldInformation.GetValue(MyCustomer).ToString();
                }
                else
                {
                    txbTextBox.Text = GetPropertyValue(MyCustomer, NameAbr).ToString();
                }
            }
        }

        private void btnSetValue_Click(object sender, RoutedEventArgs e)
        {
            //MemberInfo info = MyCustomer.GetType().FindMembers(MemberTypes.Property, BindingFlags.Public | BindingFlags.Instance, new MemberFilter(DelegateToSearchCriteria), Name).FirstOrDefault();

            // PropertyInfo propertyInfo = info as PropertyInfo;
            if (PropertyInformation != null)
            {
                PropertyInformation.SetValue(MyCustomer, txbTextBox.Text);
            }
            else if (FieldInformation != null)
            {
                FieldInformation.SetValue(MyCustomer, txbTextBox.Text);
            }
        }

        public object GetPropertyValue(object obj, string propertyName)
        {
            foreach (var prop in propertyName.Split('.').Select(s => obj.GetType().GetProperty(s)))
                obj = prop.GetValue(obj, null);

            return obj;
        }
    }
}