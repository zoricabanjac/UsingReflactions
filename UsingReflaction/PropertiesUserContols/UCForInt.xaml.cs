using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using UsingReflection.Entities;

namespace UsingReflection.PropertiesUserContols
{
    /// <summary>
    /// Interaction logic for UserControlForString.xaml
    /// </summary>
    public partial class UCForInt : PropertyValueBase
    {
        public string ControlName
        {
            get
            {
                return "txb" + NameAbr;
            }
        }

        public UCForInt()
        {
            InitializeComponent();
        }

        public UCForInt(MemberInfo info, StackPanel stackPanelInfo)
            : this()
        {
            this.MemberInformation = info;
            this.StackPanelInfo = stackPanelInfo;

            if (!TypeFullName.Contains("System"))
            {
                btnSetValue.IsEnabled = false;
            }

            lblLabel.Text = NameAbr;

            if (DataHolder.Instance.SelectedKey != null)
            {
                FillUIElements();
            }
        }

        private void FillUIElements()
        {
            if (PropertyInformation != null)
            {
                if (PropertyInformation.PropertyType.FullName.Contains("System"))
                {
                    txbTextBox.Text = PropertyInformation.GetValue(DataHolder.Instance.SelectedObject) != null ? PropertyInformation.GetValue(DataHolder.Instance.SelectedObject).ToString() : string.Empty;
                }
                else
                {
                    txbTextBox.Text = GetPropertyValue(DataHolder.Instance.SelectedObject, NameAbr) != null ? GetPropertyValue(DataHolder.Instance.SelectedObject, NameAbr).ToString() : string.Empty;
                }
            }
            else if (FieldInformation != null)
            {
                if (FieldInformation.FieldType.FullName.Contains("System"))
                {
                    txbTextBox.Text = FieldInformation.GetValue(DataHolder.Instance.SelectedObject) != null ? FieldInformation.GetValue(DataHolder.Instance.SelectedObject).ToString() : string.Empty;
                }
                else
                {
                    txbTextBox.Text = GetPropertyValue(DataHolder.Instance.SelectedObject, NameAbr) != null ? GetPropertyValue(DataHolder.Instance.SelectedObject, NameAbr).ToString() : string.Empty;
                }
            }
        }

        private void btnGetValue_Click(object sender, RoutedEventArgs e)
        {
            if (PropertyInformation != null)
            {
                if (PropertyInformation.PropertyType.FullName.Contains("System"))
                {
                    txbTextBox.Text = PropertyInformation.GetValue(DataHolder.Instance.SelectedObject) != null ? PropertyInformation.GetValue(DataHolder.Instance.SelectedObject).ToString() : string.Empty;
                }
                else
                {
                    txbTextBox.Text = GetPropertyValue(DataHolder.Instance.SelectedObject, NameAbr) != null ? GetPropertyValue(DataHolder.Instance.SelectedObject, NameAbr).ToString() : string.Empty;
                }
            }
            else if (FieldInformation != null)
            {
                if (FieldInformation.FieldType.FullName.Contains("System"))
                {
                    txbTextBox.Text = FieldInformation.GetValue(DataHolder.Instance.SelectedObject) != null ? FieldInformation.GetValue(DataHolder.Instance.SelectedObject).ToString() : string.Empty;
                }
                else
                {
                    txbTextBox.Text = GetPropertyValue(DataHolder.Instance.SelectedObject, NameAbr) != null ? GetPropertyValue(DataHolder.Instance.SelectedObject, NameAbr).ToString() : string.Empty;
                }
            }
        }

        private void btnSetValue_Click(object sender, RoutedEventArgs e)
        {
            string value = txbTextBox.Text;
            int returnInt;

            if (!int.TryParse(value, out returnInt))
            {
                MessageBox.Show("Enter the number", "Error");
            }
            else
            {
                if (PropertyInformation != null)
                {
                    PropertyInformation.SetValue(DataHolder.Instance.SelectedObject, returnInt);
                }
                else if (FieldInformation != null)
                {
                    FieldInformation.SetValue(DataHolder.Instance.SelectedObject, returnInt);
                }
            }
        }

        public object GetPropertyValue(object obj, string propertyName)
        {
            foreach (var prop in propertyName.Split('.').Select(s => obj.GetType().GetProperty(s)))
            {
                obj = prop.GetValue(obj, null);
            }

            return obj;
        }
    }
}