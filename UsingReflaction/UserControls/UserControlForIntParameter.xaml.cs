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
using UsingReflaction.Entities;

namespace UsingReflaction.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlForIntParameter.xaml
    /// </summary>
    public partial class UserControlForIntParameter : UserControl
    {
        public UserControlForIntParameter()
        {
            InitializeComponent();
        }

        public int ParameterValue
        {
            get
            {
                string value = txbTextBox.Text;
                int returnInt;

                if (int.TryParse(value, out returnInt))
                {
                    return returnInt;
                }
                else
                {
                    MessageBox.Show("Enter the number", "Error");
                    throw new Exception();  
                }
            }
        }

        public UserControlForIntParameter(MyParameterInfo parameter)
            : this()
        {
            lblParameterName.Text = parameter.ParameterName;
        }
    }
}
