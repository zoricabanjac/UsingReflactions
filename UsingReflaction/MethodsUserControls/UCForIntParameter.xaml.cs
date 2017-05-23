using System;
using System.Windows;
using System.Windows.Controls;
using UsingReflection.Entities;

namespace UsingReflection.MethodsUserContols
{
    /// <summary>
    /// Interaction logic for UserControlForIntParameter.xaml
    /// </summary>
    public partial class UCForIntParameter : UserControl
    {
        public UCForIntParameter()
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

        public UCForIntParameter(MyParameterInfo parameter)
            : this()
        {
            lblParameterName.Text = parameter.ParameterName;
        }
    }
}
