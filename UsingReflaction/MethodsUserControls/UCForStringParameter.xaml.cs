using System.Windows.Controls;
using UsingReflection.Entities;

namespace UsingReflection.MethodsUserContols
{
    public partial class UCForStringParameter : UserControl
    {
        public string ParameterValue
        {
            get
            {
                return txbTextBox.Text;
            }
        }

        public UCForStringParameter()
        {
            InitializeComponent();
        }

        public UCForStringParameter(MyParameterInfo parameter)
            : this()
        {
            lblParameterName.Text = parameter.ParameterName;
        }
    }
}
