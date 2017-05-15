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

namespace UsingReflaction.UserControls
{
    /// <summary>
    /// Interaction logic for MethodContainer.xaml
    /// </summary>
    public partial class MethodContainer : UserControl
    {
        public Object MyObject { get; set; }
        public Object MyMethResults { get; set; }
        public Helper.MethodType MethodTypeInfo { get; set; }

        public MethodContainer()
        {
            InitializeComponent();
        }

        public MethodContainer(Object obj, Helper.MethodType methodType)
            : this()
        {
            MyMethodInfo myMethodInfo = null;
            MyConstructorInfo myConstructorInfo = null;
            List<MyParameterInfo> parametersList = null;

            if (methodType == Helper.MethodType.MethodType)
            {
                myMethodInfo = obj as MyMethodInfo;
                parametersList = myMethodInfo.Parameters;
                lblMethodName.Text = myMethodInfo.MethodName;
            }
            else if (methodType == Helper.MethodType.ConstructorType)
            {
                myConstructorInfo = obj as MyConstructorInfo;
                parametersList = myConstructorInfo.Parameters;
                lblMethodName.Text = myConstructorInfo.ConstructorName;
            }

            MethodTypeInfo = methodType;

            foreach (MyParameterInfo parameter in parametersList)
            {
                if (string.Equals(parameter.ParameterType, "String"))
                {
                    UserControls.UserControlForStringParameter control = new UserControls.UserControlForStringParameter(parameter);
                    stpMethodParametersContainer.Children.Add(control);
                }
                else if (string.Equals(parameter.ParameterType, "Int32"))
                {
                    UserControls.UserControlForIntParameter control = new UserControls.UserControlForIntParameter(parameter);
                    stpMethodParametersContainer.Children.Add(control);
                }
                else
                {
                    return;
                }
            }

            MyObject = obj;
        }

        private void btnMethodRun_Click(object sender, RoutedEventArgs e)
        {
            int childrenCount = stpMethodParametersContainer.Children.Count;
            object[] args = new object[childrenCount];
            for (int i = 0; i < childrenCount; i++)
            {
                if (stpMethodParametersContainer.Children[i] is UserControlForStringParameter)
                {
                    UserControlForStringParameter controlElement = stpMethodParametersContainer.Children[i] as UserControlForStringParameter;
                    args[i] = controlElement.ParameterValue;
                }

                if (stpMethodParametersContainer.Children[i] is UserControlForIntParameter)
                {
                    try
                    {
                        UserControlForIntParameter controlElement = stpMethodParametersContainer.Children[i] as UserControlForIntParameter;
                        args[i] = controlElement.ParameterValue;
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }
            }

            MyMethodInfo myMethodInfo = null;
            MyConstructorInfo myConstructorInfo = null;

            if (MethodTypeInfo == Helper.MethodType.MethodType)
            {
                myMethodInfo = MyObject as MyMethodInfo;

                if (myMethodInfo != null)
                {
                    Object result = myMethodInfo.MethodInfo.Invoke(myMethodInfo.SelectedObject, myMethodInfo.Parameters.Count == 0 ? null : args);

                    if (myMethodInfo.ReturnType == "System.String" && result != null)
                    {
                        txbResult.Text = result.ToString();
                    }
                }
            }
            else
            {
                myConstructorInfo = MyObject as MyConstructorInfo;

                if (myConstructorInfo != null)
                {
                    myConstructorInfo.ConstructorInfo.Invoke(myConstructorInfo.SelectedObject, myConstructorInfo.Parameters.Count == 0 ? null : args);
                }
            }         
        }
    }
}
