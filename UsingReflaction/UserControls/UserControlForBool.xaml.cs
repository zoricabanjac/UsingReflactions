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
    public partial class UserControlForBool : UserControlBase
    {

        public string ControlName
        {
            get
            {
                return "chk" + NameAbr;
            }
        }

        public UserControlForBool()
        {
            InitializeComponent();
        }

        public UserControlForBool(MemberInfo info, StackPanel stackPanelInfo) : this()
        {
            this.MemberInformation = info;
            this.StackPanelInfo = stackPanelInfo;
            lblLabel.Text = NameAbr;
           
            //chkCheckBox.IsChecked = myobj.isPrivileged;
        }

        public object GetPropertyValue(object obj, string propertyName)
        {
            foreach (var prop in propertyName.Split('.').Select(s => obj.GetType().GetProperty(s)))
                obj = prop.GetValue(obj, null);

            return obj;
        }


        private void chkCheckBox_CheckChanged(object sender, RoutedEventArgs e)
        {
            if (PropertyInformation != null)
            {
                PropertyInformation.SetValue(DataHolder.SelectedObject, chkCheckBox.IsChecked);
            }
            else if (FieldInformation != null)
            {
                FieldInformation.SetValue(DataHolder.SelectedObject, chkCheckBox.IsChecked);
            }
        }

        private void chkCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
