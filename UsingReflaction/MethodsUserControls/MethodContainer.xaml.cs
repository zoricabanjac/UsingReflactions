using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UsingReflection.Entities;
using UsingReflection.PropertiesUserContols;

namespace UsingReflection.MethodsUserContols
{
    public partial class MethodContainer : UserControl
    {
        public bool ForceEditEnabled
        {
            get { return (bool)GetValue(ForceEditEnabledProperty); }
            set { SetValue(ForceEditEnabledProperty, value); }
        }

        public static readonly DependencyProperty ForceEditEnabledProperty =
            DependencyProperty.Register("ForceEditEnabled", typeof(bool), typeof(UCForBool), new PropertyMetadata(false));

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
                    MethodsUserContols.UCForStringParameter control = new MethodsUserContols.UCForStringParameter(parameter);
                    stpMethodParametersContainer.Children.Add(control);
                }
                else if (string.Equals(parameter.ParameterType, "Int32"))
                {
                    MethodsUserContols.UCForIntParameter control = new MethodsUserContols.UCForIntParameter(parameter);
                    stpMethodParametersContainer.Children.Add(control);
                }
                else if (string.Equals(parameter.ParameterType, "Boolean"))
                {
                    MethodsUserContols.UCForBoolParameter control = new MethodsUserContols.UCForBoolParameter(parameter);
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
                if (stpMethodParametersContainer.Children[i] is UCForStringParameter)
                {
                    UCForStringParameter controlElement = stpMethodParametersContainer.Children[i] as UCForStringParameter;
                    args[i] = controlElement.ParameterValue;
                }
                if (stpMethodParametersContainer.Children[i] is UCForIntParameter)
                {
                    UCForIntParameter controlElement = stpMethodParametersContainer.Children[i] as UCForIntParameter;
                    args[i] = controlElement.ParameterValue;
                }

                if (stpMethodParametersContainer.Children[i] is UCForBoolParameter)
                {
                    UCForBoolParameter controlElement = stpMethodParametersContainer.Children[i] as UCForBoolParameter;
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

                    if ((myMethodInfo.ReturnType == "System.String" || myMethodInfo.ReturnType == "System.Int32" || myMethodInfo.ReturnType == "System.Boolean") && result != null)
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
