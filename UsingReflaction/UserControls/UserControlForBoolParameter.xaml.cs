using System.Windows.Controls;
using UsingReflection.Entities;

namespace UsingReflection.UserControls
{
    public partial class UserControlForBoolParameter : UserControl
    {
        public bool? ParameterValue
        {
            get
            {
                return chkCheckBox.IsChecked;
            }
        }

        public UserControlForBoolParameter()
        {
            InitializeComponent();
        }

        public UserControlForBoolParameter(MyParameterInfo parameter)
            : this()
        {
            lblParameterName.Text = parameter.ParameterName;
        }
    }
}
