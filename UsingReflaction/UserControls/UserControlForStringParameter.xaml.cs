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

namespace UsingReflaction.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlForStringParameter.xaml
    /// </summary>
    public partial class UserControlForStringParameter : UserControl
    {
        private System.Reflection.MethodInfo info;
        private StackPanel stackPanelInfo;
        private TestEntities.Customer myCustomer;
        private Entities.MyParameterInfo myParameterInfo;

        public UserControlForStringParameter()
        {
            InitializeComponent();
        }

        public UserControlForStringParameter(System.Reflection.MethodInfo info, StackPanel stackPanelInfo, TestEntities.Customer myCustomer, Entities.MyParameterInfo myParameterInfo)
        {
            // TODO: Complete member initialization
            this.info = info;
            this.stackPanelInfo = stackPanelInfo;
            this.myCustomer = myCustomer;
            this.myParameterInfo = myParameterInfo;
        }
    }
}
