using System.Windows.Controls;
using UsingReflaction.Entities;

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
