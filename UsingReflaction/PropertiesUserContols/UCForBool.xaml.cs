using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using UsingReflection.Entities;

namespace UsingReflection.PropertiesUserContols
{
    public partial class UCForBool : PropertyValueBase
    {
        public string ControlName
        {
            get
            {
                return "chk" + NameAbr;
            }
        }

        public UCForBool()
        {
            InitializeComponent();
        }

        public UCForBool(MemberInfo info, StackPanel stackPanelInfo)
            : this()
        {
            this.MemberInformation = info;
            this.StackPanelInfo = stackPanelInfo;
            lblLabel.Text = NameAbr;
        }

        public object GetPropertyValue(object obj, string propertyName)
        {
            foreach (var prop in propertyName.Split('.').Select(s => obj.GetType().GetProperty(s)))
            {
                obj = prop.GetValue(obj, null);
            }

            return obj;
        }

        private void btnGetValue_Click(object sender, RoutedEventArgs e)
        {
            if (PropertyInformation != null)
            {
                if (PropertyInformation.PropertyType.FullName.Contains("System"))
                {
                    if (PropertyInformation.GetValue(DataHolder.Instance.SelectedObject) != null)
                    {
                        chkCheckBox.IsChecked = (bool)PropertyInformation.GetValue(DataHolder.Instance.SelectedObject);
                    }
                }
                else
                {
                    if (GetPropertyValue(DataHolder.Instance.SelectedObject, NameAbr) != null)
                    {
                        chkCheckBox.IsChecked = (bool)GetPropertyValue(DataHolder.Instance.SelectedObject, NameAbr);
                    }
                }
            }
            else if (FieldInformation != null)
            {
                if (FieldInformation.FieldType.FullName.Contains("System"))
                {
                    if (FieldInformation.GetValue(DataHolder.Instance.SelectedObject) != null)
                    {
                        chkCheckBox.IsChecked = (bool)FieldInformation.GetValue(DataHolder.Instance.SelectedObject);
                    }
                }
                else
                {
                    if (GetPropertyValue(DataHolder.Instance.SelectedObject, NameAbr) != null)
                    {
                        chkCheckBox.IsChecked = (bool)GetPropertyValue(DataHolder.Instance.SelectedObject, NameAbr);
                    }
                }
            }
        }

        private void btnSetValue_Click(object sender, RoutedEventArgs e)
        {
            if (PropertyInformation != null)
            {
                PropertyInformation.SetValue(DataHolder.Instance.SelectedObject, chkCheckBox.IsChecked);
            }
            else if (FieldInformation != null)
            {
                FieldInformation.SetValue(DataHolder.Instance.SelectedObject, chkCheckBox.IsChecked);
            }
        }

        private void chkCheckBox_CheckChanged(object sender, RoutedEventArgs e)
        {
            if (PropertyInformation != null)
            {
                PropertyInformation.SetValue(DataHolder.Instance.SelectedObject, chkCheckBox.IsChecked);
            }
            else if (FieldInformation != null)
            {
                FieldInformation.SetValue(DataHolder.Instance.SelectedObject, chkCheckBox.IsChecked);
            }
        }
    }
}
