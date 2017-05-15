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
    /// Interaction logic for UserControlForStringParameter.xaml
    /// </summary>
    public partial class UserControlForStringParameter : UserControl
    {
        public string ParameterValue
        {
            get
            {
                return txbTextBox.Text;
            }
        }
        public UserControlForStringParameter()
        {
            InitializeComponent();
        }

        public UserControlForStringParameter(MyParameterInfo parameter)
            : this()
        {
            lblParameterName.Text = parameter.ParameterName;
        }
    }
}
