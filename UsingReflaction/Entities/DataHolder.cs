using System;
using System.Collections.Generic;
using System.ComponentModel;
using UsingReflection.TestEntities;

namespace UsingReflection.Entities
{
    public class DataHolder : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public static DataHolder Instance { get; set; }

        static DataHolder()
        {
            Instance = new DataHolder();
        }

        private DataHolder()
        {
            Data = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Data { get; set; }

        public bool HasSelectedObject
        {
            get
            {
                return SelectedKey != null;
            }
        }

        public Object SelectedObject
        {
            get
            {
                return Data[SelectedKey];
            }
        }


        private string selectedKey = null;
        public string SelectedKey
        {
            get
            {
                return selectedKey;
            }
            set
            {
                selectedKey = value;
                RaisePropertyChanged("SelectedKey");
                RaisePropertyChanged("SelectedObject");
                RaisePropertyChanged("HasSelectedObject");
            }
        }

        public void CreateNewObject(string instanceName, object instance)
        {
            if (!Data.ContainsKey(instanceName))
            {
                Data.Add(instanceName, instance);
            }
        }

        internal void CreateTestCustomer()
        {
            List<Order> orders = new List<Order>();
            Order order = new Order(1, "banana", 1.5);
            Order order1 = new Order(2, "ananas", 2);
            orders.Add(order);
            orders.Add(order1);

            Customer newCustomer = new Customer("Zorica", "Banjac", orders.ToArray());
            newCustomer.EmailAddress = "zorica.banjac@gmail.com";
            newCustomer.isPrivileged = true;

            var address = new Customer.Address();
            address.Street = "M.Dakica 47";
            address.City = "Ruma";
            address.Zip = "22400";
            address.State = "Serbia";
            newCustomer.MailingAddress = address;

            Data.Add("TestCustomer", newCustomer);
            SelectedKey = "TestCustomer";
        }

        internal void CreateTestPerson()
        {
            Person newPerson = new Person();
            newPerson.FirstName = "Zorica";
            newPerson.LastName = "Banjac";

            Data.Add("TestPerson", newPerson);
            SelectedKey = "TestPerson";
        }
    }
}
