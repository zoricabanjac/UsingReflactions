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

        public MainWindow()
        {
            InitializeComponent();

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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            MyClassInfo myClass = new MyClassInfo();
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

            //List<MyNestedTypeInfo> nestedList = new List<MyNestedTypeInfo>();
            //foreach (Type nestedType in type.GetNestedTypes())
            //{
            //    nestedList.Add(new MyNestedTypeInfo(nestedType.ToString()));
            //}
            //myClass.NestedTupesList = nestedList;

            List<MyFieldInfo> myFields = new List<MyFieldInfo>();
            foreach (FieldInfo info in type.GetFields())
            {
                string infoType = (((FieldInfo)(info)).FieldType).FullName;
                string infoName = ((FieldInfo)(info)).Name;

                MakeLabelsAndTextBoxes(info, stpFields);

                myFields.Add(new MyFieldInfo(info, infoName, infoType, info.MemberType));
            }
            myClass.FieldsList = myFields;

            List<MyPropertyInfo> myProperties = new List<MyPropertyInfo>();
            foreach (PropertyInfo info in type.GetProperties())
            {
                myProperties.Add(new MyPropertyInfo(info, info.MemberType, info.PropertyType.FullName, ((MemberInfo)(info)).Name));
                MakeLabelsAndTextBoxes(info, stpProperties);
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

            List<MyMethodInfo> myMethods = new List<MyMethodInfo>();
            foreach (MethodInfo info in type.GetMethods())
            {
                string returntype = info.ReturnType.FullName;
                string name = info.Name;

                List<MyParameterInfo> parameters = new List<MyParameterInfo>();
                foreach (ParameterInfo pParameter in info.GetParameters())
                {
                    parameters.Add(new MyParameterInfo(pParameter.ParameterType.Name, pParameter.Name));
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

        private void MakeLabelsAndTextBoxes(MemberInfo info, StackPanel stackPanel)
        {
            Label label = new Label();
            label.Content = ((MemberInfo)(info)).Name;
            stackPanel.Children.Add(label);

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

            if (string.Equals(fullName, "System.String"))
            {
                TextBox textBox = new TextBox();
                textBox.Name = "txb" + ((MemberInfo)(info)).Name;
                stackPanel.Children.Add(textBox);

                Button getButton = new Button();
                getButton.Content = "Get value";
                getButton.Name = "btn" + ((MemberInfo)(info)).Name;
                getButton.Width = 75;
                getButton.Height = 20;
                getButton.HorizontalAlignment = HorizontalAlignment.Center;
                getButton.Click += new RoutedEventHandler(getButton_Click);
                stackPanel.Children.Add(getButton);

                if (fullName.Contains("System"))
                {
                    Button setButton = new Button();
                    setButton.Content = "Set value";
                    setButton.Name = "btn" + ((MemberInfo)(info)).Name;
                    setButton.Width = 75;
                    setButton.Height = 20;
                    setButton.HorizontalAlignment = HorizontalAlignment.Center;
                    setButton.Click += new RoutedEventHandler(setButton_Click);
                    stackPanel.Children.Add(setButton);
                }
            }
            else if (string.Equals(fullName, "System.Boolean"))
            {
                CheckBox checkBox= new CheckBox();
                checkBox.Name = "chk" + ((MemberInfo)(info)).Name;
                checkBox.Checked += new RoutedEventHandler(getCheckbox_Click);
                stackPanel.Children.Add(checkBox);    
            }           
        }

        private void getCheckbox_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void setButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string clearName = button.Name.Substring(3);

            MemberInfo info = myCustomer.GetType().FindMembers(MemberTypes.Property, BindingFlags.Public | BindingFlags.Instance, new MemberFilter(DelegateToSearchCriteria), clearName).FirstOrDefault();
            foreach (object child in stpProperties.Children)
            {
                if (child is TextBox)
                {
                    TextBox textBox = child as TextBox;
                    if (textBox.Name.Contains(clearName))
                    {
                        PropertyInfo propertyInfo = info as PropertyInfo;
                        propertyInfo.SetValue(myCustomer, textBox.Text);
                    }
                }
            }
        }

        private void getButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string clearName = button.Name.Substring(3);

            MemberInfo info = myCustomer.GetType().FindMembers(MemberTypes.Property | MemberTypes.Field, BindingFlags.Public | BindingFlags.Instance, new MemberFilter(DelegateToSearchCriteria), clearName).FirstOrDefault();
            foreach (object child in stpProperties.Children)
            {
                if (child is TextBox)
                {
                    TextBox textBox = child as TextBox;
                    if (textBox.Name.Contains(clearName))
                    {
                        if (info is PropertyInfo)
                        {
                            PropertyInfo propertyInfo = info as PropertyInfo;
                            {
                                if (propertyInfo.PropertyType.FullName.Contains("System"))
                                {
                                    textBox.Text = propertyInfo.GetValue(myCustomer).ToString();
                                }
                                else
                                {
                                    textBox.Text = GetPropertyValue(myCustomer, clearName).ToString();
                                }
                            }
                        }
                        else
                        {
                            FieldInfo fieldInfo = info as FieldInfo;
                            {
                                if (fieldInfo.FieldType.FullName.Contains("System"))
                                {
                                    textBox.Text = fieldInfo.GetValue(myCustomer).ToString();
                                }
                                else
                                {
                                    //extBox.Text = myCustomer, clearName).ToString();
                                }
                            }
                        }
                    }
                }
                else if (child is CheckBox)
                {
                    CheckBox chkbox = child as CheckBox;

                    if (chkbox.Name.Contains(clearName))
                    {
                        if (info is PropertyInfo)
                        {
                            PropertyInfo propertyInfo = info as PropertyInfo;
                            {
                                if (propertyInfo.PropertyType.FullName.Contains("System"))
                                {
                                    chkbox.IsChecked = (bool)propertyInfo.GetValue(myCustomer);
                                }
                                else
                                {
                                    //textBox.Text = GetPropertyValue(myCustomer, clearName).ToString();
                                }
                            }
                        }
                        else
                        {
                            FieldInfo fieldInfo = info as FieldInfo;
                            {
                                if (fieldInfo.FieldType.FullName.Contains("System"))
                                {
                                    chkbox.IsChecked = (bool)fieldInfo.GetValue(myCustomer);
                                }
                                else
                                {
                                    //extBox.Text = myCustomer, clearName).ToString();
                                }
                            }
                        }

                    }

                }
            }
        }

        public object GetPropertyValue(object obj, string propertyName)
        {
            foreach (var prop in propertyName.Split('.').Select(s => obj.GetType().GetProperty(s)))
                obj = prop.GetValue(obj, null);

            return obj;
        }


        private bool DelegateToSearchCriteria(MemberInfo objmemberInfo, object objSearch)
        {
            if (objmemberInfo.Name.ToString() == objSearch.ToString())
                return true;
            else
                return false;
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
    }
}
