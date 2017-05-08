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
using UsingReflaction.TestEntities;

namespace UsingReflaction
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Customer myCustomer;

        MyClassInfo myClass;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            myClass = new MyClassInfo();
            Type type = GetType(txtClassName.Text);

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

            List<MyFieldInfo> myFields = new List<MyFieldInfo>();
            foreach (FieldInfo info in type.GetFields())
            {
                string infoType = (((FieldInfo)(info)).FieldType).FullName;
                string infoName = ((FieldInfo)(info)).Name;

                DesignControls(info, stpFieldsDynamic);

                myFields.Add(new MyFieldInfo(info, infoName, infoType, info.MemberType));
            }
            myClass.FieldsList = myFields;

            List<MyPropertyInfo> myProperties = new List<MyPropertyInfo>();
            foreach (PropertyInfo info in type.GetProperties())
            {
                myProperties.Add(new MyPropertyInfo(info, info.MemberType, info.PropertyType.FullName, ((MemberInfo)(info)).Name));
                DesignControls(info, stpPropertiesDynamic);
            }

            myClass.PropertiesList = myProperties;

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
            myClass.ConstructorsList = myConstructors;

            dynamic result = null;
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
                    DesignControlForMathodParameter(info, pParameter, stpMethodsDynamic, parameter);
                }

                //Button getButton = new Button();
                //getButton.Content = "Calculate";
                //getButton.Name = "btnCalculate";
                //getButton.Width = 75;
                //getButton.Height = 20;
                //getButton.HorizontalAlignment = HorizontalAlignment.Center;
                //getButton.Click += new RoutedEventHandler(btnCalculate_Click);
                //stpMethodsDynamic.Children.Add(getButton);

                object classInstance = Activator.CreateInstance(type, null);

                if (parameters.All(it => it.ParameterType.Contains("String")))
                {

                    //object[] args = {}
                    //result = info.Invoke(classInstance, parameters.Count == 0 ? null : parametersArray);
                }
                myMethods.Add(new MyMethodInfo(info.MemberType, info, returntype, name, parameters));
            }

            myClass.MethodsList = myMethods;

            List<MyEventInfo> myEvents = new List<MyEventInfo>();
            foreach (EventInfo info in type.GetEvents())
            {
                myEvents.Add(new MyEventInfo(info.MemberType, info, info.ToString()));
            }

            myClass.EventsList = myEvents;

            dgProperties.ItemsSource = myClass.PropertiesList;
            dgFields.ItemsSource = myClass.FieldsList;
            dgMethods.ItemsSource = myClass.MethodsList;
            dgConstructors.ItemsSource = myClass.ConstructorsList;
            dgEvents.ItemsSource = myClass.EventsList;
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
           //
        }

        private void DesignControlForMathodParameter(MethodInfo info, ParameterInfo parameterInfo, StackPanel stackPanelInfo, MyParameterInfo myParameterInfo)
        {
            if (string.Equals(myParameterInfo.ParameterType, "System.String"))
            {
                UserControls.UserControlForStringParameter control = new UserControls.UserControlForStringParameter(info, stackPanelInfo, myCustomer, myParameterInfo);
                stackPanelInfo.Children.Add(control);
            }
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

            if (string.Equals(fullName, "System.String") || !fullName.Contains("System"))
            {
                UserControls.UserControlForString control = new UserControls.UserControlForString(info, stackPanelInfo, myCustomer);
                stackPanelInfo.Children.Add(control);
            }

            if (string.Equals(fullName, "System.Boolean"))
            {
                UserControls.UserControlForBool control = new UserControls.UserControlForBool(info, stackPanelInfo, myCustomer);
                stackPanelInfo.Children.Add(control);
            }
        }

        private void getCheckbox_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                    return type;
            }
            return null;
        }

        private void btnNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            List<Order> orders = new List<Order>();
            Order order = new Order(1, "banana", 1.5);
            Order order1 = new Order(2, "ananas", 2);
            orders.Add(order);
            orders.Add(order1);

            myCustomer = new Customer("Zorica", "Banjac", orders.ToArray());
            myCustomer.EmailAddress = "zorica.banjac@gmail.com";
            myCustomer.isPrivileged = true;

            var address = new Customer.Address();
            address.Street = "M.Dakica 47";
            address.City = "Ruma";
            address.Zip = "22400";
            address.State = "Serbia";
            myCustomer.MailingAddress = address;
        }
    }
}
