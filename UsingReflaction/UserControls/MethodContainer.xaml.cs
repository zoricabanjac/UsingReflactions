using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UsingReflection.Entities;

namespace UsingReflection.UserControls
{
    public partial class MethodContainer : UserControl
    {
        public bool ForceEditEnabled
        {
            get { return (bool)GetValue(ForceEditEnabledProperty); }
            set { SetValue(ForceEditEnabledProperty, value); }
        }

        public static readonly DependencyProperty ForceEditEnabledProperty =
            DependencyProperty.Register("ForceEditEnabled", typeof(bool), typeof(UserControlForBool), new PropertyMetadata(false));

        public Object MyObject { get; set; }
        public Helper.MethodType MethodTypeInfo { get; set; }

        public event EventHandler MethodExecuted;
        private void RaiseMethodExecuted()
        {
            if (MethodExecuted != null)
            {
                MethodExecuted(this, new EventArgs());
            }
        }

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
                txbVariableName.Visibility = Visibility.Visible;
                lblVariable.Visibility = Visibility.Visible;
                ForceEditEnabled = true;
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
                else if (string.Equals(parameter.ParameterType, "Boolean"))
                {
                    UserControls.UserControlForBoolParameter control = new UserControls.UserControlForBoolParameter(parameter);
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

                    UserControlForIntParameter controlElement = stpMethodParametersContainer.Children[i] as UserControlForIntParameter;
                    args[i] = controlElement.ParameterValue;
                }

                if (stpMethodParametersContainer.Children[i] is UserControlForBoolParameter)
                {

                    UserControlForBoolParameter controlElement = stpMethodParametersContainer.Children[i] as UserControlForBoolParameter;
                    args[i] = controlElement.ParameterValue;
                }
            }

            MyMethodInfo myMethodInfo = null;
            MyConstructorInfo myConstructorInfo = null;

            if (MethodTypeInfo == Helper.MethodType.MethodType)
            {
                myMethodInfo = MyObject as MyMethodInfo;

                if (myMethodInfo != null)
                {
                    Object result = myMethodInfo.MethodInfo.Invoke(DataHolder.Instance.SelectedObject, myMethodInfo.Parameters.Count == 0 ? null : args);

                    if (myMethodInfo.ReturnType == "System.String" && result != null)
                    {
                        txbResult.Text = result.ToString();
                    }

                    RaiseMethodExecuted();
                }
            }
            else
            {
                myConstructorInfo = MyObject as MyConstructorInfo;

                Type type = Helper.GetType((myConstructorInfo.ConstructorName));
                var instance = Activator.CreateInstance(type);

                if (myConstructorInfo != null)
                {
                    myConstructorInfo.ConstructorInfo.Invoke(instance, myConstructorInfo.Parameters.Count == 0 ? null : args);
                }

                DataHolder.Instance.CreateNewObject(txbVariableName.Text, instance);
                RaiseMethodExecuted();
            }
        }
    }
}
