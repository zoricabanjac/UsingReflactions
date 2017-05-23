using System.Windows;
using System.Windows.Controls;
using UsingReflection.PropertiesUserContols;

namespace UsingReflection.MethodsUserContols
{
    public class MethodParametersValueBase : UserControl
    {
        public bool ForceEditEnabled
        {
            get { return (bool)GetValue(ForceEditEnabledProperty); }
            set { SetValue(ForceEditEnabledProperty, value); }
        }

        public static readonly DependencyProperty ForceEditEnabledProperty =
            DependencyProperty.Register("ForceEditEnabled", typeof(bool), typeof(UCForBool), new PropertyMetadata(false));

    }
}
