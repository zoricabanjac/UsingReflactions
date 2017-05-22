using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using UsingReflection.Entities;
using System.Linq;

namespace UsingReflection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> TypeOptions { get; set; }
        private ObservableCollection<string> NameSpaceOptions { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            TypeOptions = new ObservableCollection<string>();
            NameSpaceOptions = new ObservableCollection<string>();
            NameSpaceOptions.Add("UsingReflection");
            NameSpaceOptions.Add("UsingReflection.TestEntities");
            cbTypeClass.ItemsSource = TypeOptions;
            cbNamespace.ItemsSource = NameSpaceOptions;
        }

        private void btnSearchNamespace_Click(object sender, RoutedEventArgs e)
        {
            var types = from type in Assembly.GetExecutingAssembly().GetTypes()
                    where type.IsClass && type.Namespace == cbNamespace.Text
                    select type;

            foreach (var type in types)
            {
                if (!TypeOptions.Contains(type.ToString()))
                {
                    TypeOptions.Add(type.ToString());
                }
            }
        }

        private void btnSearchClass_Click(object sender, RoutedEventArgs e)
        {
            DataHolder.Instance.SelectedKey = null;
            Type type = Helper.GetType(cbTypeClass.Text);
            if (type == null)
            {
                return;
            }
         
            MakeElements(type);     
        }

        private void MakeElements(Type type)
        {
            ClearUIElements();
            if (!NameSpaceOptions.Contains(cbNamespace.Text))
            {
                NameSpaceOptions.Add(cbNamespace.Text);
            }

            if (!TypeOptions.Contains(cbTypeClass.Text))
            {
                TypeOptions.Add(cbTypeClass.Text);              
            }

            MyClassInfo myClass = new MyClassInfo();
            
            MakeBaseDataElements(myClass, type);
            MakeFieldsElements(myClass, type);
            MakePropertyElements(myClass, type);
            myClass.ConstructorsList = MakeConsturctorsElements(type);
            myClass.MethodsList = MakeMethodsElements(type);
            myClass.EventsList = MakeEventsElements(type);

            dgProperties.ItemsSource = myClass.PropertiesList;
            dgFields.ItemsSource = myClass.FieldsList;
            dgMethods.ItemsSource = myClass.MethodsList;
            dgConstructors.ItemsSource = myClass.ConstructorsList;
            dgEvents.ItemsSource = myClass.EventsList;

            dgVariables.ItemsSource = DataHolder.Instance.Data;
        }

        private void ClearUIElements()
        {
            stpConstructorsDynamic.Children.Clear();
            stpFieldsDynamic.Children.Clear();
            stpMethodsDynamic.Children.Clear();
            stpPropertiesDynamic.Children.Clear();
            dgVariables.UnselectAll();
            
        }

        private static List<MyEventInfo> MakeEventsElements(Type type)
        {
            List<MyEventInfo> myEvents = new List<MyEventInfo>();
            foreach (EventInfo info in type.GetEvents())
            {
                myEvents.Add(new MyEventInfo(info.MemberType, info, info.ToString()));
            }

            return myEvents;
        }

        private static List<MyMethodInfo> MakeMethodsElements(Type type)
        {
            List<MyMethodInfo> myMethods = new List<MyMethodInfo>();
            foreach (MethodInfo info in type.GetMethods())
            {
                string returntype = info.ReturnType.FullName;
                string name = info.Name;

                List<MyParameterInfo> parameters = new List<MyParameterInfo>();
                foreach (ParameterInfo pParameter in info.GetParameters())
                {
                    MyParameterInfo parameter = new MyParameterInfo(pParameter.ParameterType.Name, pParameter.Name);
                    parameters.Add(parameter);
                }

                myMethods.Add(new MyMethodInfo(info.MemberType, info, returntype, name, parameters));
            }

            return myMethods;
        }

        private static List<MyConstructorInfo> MakeConsturctorsElements(Type type)
        {
            List<MyConstructorInfo> myConstructors = new List<MyConstructorInfo>();
            foreach (ConstructorInfo info in type.GetConstructors())
            {
                string name = info.DeclaringType.FullName;

                List<MyParameterInfo> parameters = new List<MyParameterInfo>();
                foreach (ParameterInfo pParameter in info.GetParameters())
                {
                    parameters.Add(new MyParameterInfo(pParameter.ParameterType.Name, pParameter.Name));
                }

                myConstructors.Add(new MyConstructorInfo(info.MemberType, info, name, parameters));
            }

            return myConstructors;
        }

        private void MakePropertyElements(MyClassInfo myClass, Type type)
        {
            List<MyPropertyInfo> myProperties = new List<MyPropertyInfo>();
            foreach (PropertyInfo info in type.GetProperties())
            {
                myProperties.Add(new MyPropertyInfo(info, info.MemberType, info.PropertyType.FullName, ((MemberInfo)(info)).Name));
                DesignControls(info, stpPropertiesDynamic);
            }

            myClass.PropertiesList = myProperties;
        }

        private void MakeFieldsElements(MyClassInfo myClass, Type type)
        {
            List<MyFieldInfo> myFields = new List<MyFieldInfo>();
            foreach (FieldInfo info in type.GetFields())
            {
                string infoType = (((FieldInfo)(info)).FieldType).FullName;
                string infoName = ((FieldInfo)(info)).Name;

                DesignControls(info, stpFieldsDynamic);

                myFields.Add(new MyFieldInfo(info, infoName, infoType, info.MemberType));
            }

            myClass.FieldsList = myFields;
        }

        private void MakeBaseDataElements(MyClassInfo myClass, Type type)
        {
            myClass.ClassName = type.Name;
            txbClassName.Text = myClass.ClassName;

            myClass.Assambly = type.Assembly.FullName;
            txbAssambly.Text = myClass.Assambly;

            myClass.Attributes = type.Attributes;
            txbAttributes.Text = myClass.Attributes.ToString();

            myClass.BaseType = type.BaseType.Name;
            txbBaseType.Text = myClass.BaseType;

            myClass.FullName = type.FullName;
            txbFullName.Text = myClass.FullName;

            myClass.Namespace = type.Namespace;
            txbNamespace.Text = myClass.Namespace;

            myClass.Module = type.Module.Name;
            txbModule.Text = myClass.Module;
        }

        private void DesignControlForMethod(Object obj, StackPanel stackPanel, Helper.MethodType methodInfo)
        {
            stackPanel.Children.Clear();

            UserControls.MethodContainer container = new UserControls.MethodContainer(obj, methodInfo);

            if (methodInfo == Helper.MethodType.ConstructorType)
            {
                container.MethodExecuted += Container_MethodExecuted;
            }

            stackPanel.Children.Add(container);
        }

        private void Container_MethodExecuted(object sender, EventArgs e)
        {
            dgVariables.Items.Refresh();
        }

        private void DesignControls(MemberInfo info, StackPanel stackPanelInfo)
        {
            FieldInfo fieldInfo = null;
            string fullName = null;
            PropertyInfo propertyInfo = null;
            if (info is FieldInfo)
            {
                fieldInfo = info as FieldInfo;
                fullName = fieldInfo.FieldType.FullName;
            }
            else
            {
                propertyInfo = info as PropertyInfo;
                fullName = propertyInfo.PropertyType.FullName;
            }

            if (string.Equals(fullName, "System.Int32"))
            {
                UserControls.UserControlForInt control = new UserControls.UserControlForInt(info, stackPanelInfo);
                stackPanelInfo.Children.Add(control);
            }
            else if (string.Equals(fullName, "System.Boolean"))
            {
                UserControls.UserControlForBool control = new UserControls.UserControlForBool(info, stackPanelInfo);
                stackPanelInfo.Children.Add(control);
            }
            else if(string.Equals(fullName, "System.String"))
            {
                UserControls.UserControlForString control = new UserControls.UserControlForString(info, stackPanelInfo);
                stackPanelInfo.Children.Add(control);
            }
            else
            {
                return;
            }
        }

        private void btnNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            DataHolder.Instance.CreateTestCustomer();
        }

        private void btnNewPerson_Click(object sender, RoutedEventArgs e)
        {
            DataHolder.Instance.CreateTestPerson();
        }

        private void dgVariables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var instance = e.AddedItems[0];
                DataHolder.Instance.SelectedKey = ((KeyValuePair<string, object>)instance).Key;

                Type type = Helper.GetType(DataHolder.Instance.SelectedObject.ToString());
                if (type == null)
                {
                    return;
                }

                MakeElements(type);
            }
            else
            {

            }
        }

        private void dgConsturctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var obj = e.AddedItems[0];

                DesignControlForMethod(obj, stpConstructorsDynamic, Helper.MethodType.ConstructorType);
            }
            else
            {

            }
        }

        private void dgMethods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var obj = e.AddedItems[0];

                DesignControlForMethod(obj, stpMethodsDynamic, Helper.MethodType.MethodType);
            }
            else
            {

            }
        }
    }
}
