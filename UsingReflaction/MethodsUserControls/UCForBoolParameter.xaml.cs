using System.Windows.Controls;
using UsingReflection.Entities;

namespace UsingReflection.MethodsUserContols

{
    public partial class UCForBoolParameter : UserControl
    {
        public bool? ParameterValue
        {
            get
            {
                return chkCheckBox.IsChecked;
            }
        }

        public UCForBoolParameter()
        {
            InitializeComponent();
        }

        public UCForBoolParameter(MyParameterInfo parameter)
            : this()
        {
            lblParameterName.Text = parameter.ParameterName;
        }
    }
}
