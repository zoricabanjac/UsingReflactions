using System;
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
using UsingReflaction.Entities;
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

        public UserControlForString(MemberInfo info, StackPanel stackPanelInfo)
            : this()
        {
            this.MemberInformation = info;
            this.StackPanelInfo = stackPanelInfo;

            if (!TypeFullName.Contains("System"))
            {
                btnSetValue.IsEnabled = false;
            }
            
            lblLabel.Text = NameAbr;
        }

        private void btnGetValue_Click(object sender, RoutedEventArgs e)
        {
            if (PropertyInformation != null)
            {
                if (PropertyInformation.PropertyType.FullName.Contains("System"))
                {
                    txbTextBox.Text = PropertyInformation.GetValue(DataHolder.Instance.SelectedObject).ToString();
                }
                else
                {
                    txbTextBox.Text = GetPropertyValue(DataHolder.Instance.SelectedObject, NameAbr).ToString();
                }
            }
            else if (FieldInformation != null)
            {
                if (FieldInformation.FieldType.FullName.Contains("System"))
                {
                    txbTextBox.Text = FieldInformation.GetValue(DataHolder.Instance.SelectedObject).ToString();
                }
                else
                {
                    txbTextBox.Text = GetPropertyValue(DataHolder.Instance.SelectedObject, NameAbr).ToString();
                }
            }
        }

        private void btnSetValue_Click(object sender, RoutedEventArgs e)
        {
            if (PropertyInformation != null)
            {
                PropertyInformation.SetValue(DataHolder.Instance.SelectedObject, txbTextBox.Text);
            }
            else if (FieldInformation != null)
            {
                FieldInformation.SetValue(DataHolder.Instance.SelectedObject, txbTextBox.Text);
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