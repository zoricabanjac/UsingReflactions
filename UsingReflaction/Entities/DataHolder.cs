using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsingReflaction.TestEntities;

namespace UsingReflaction.Entities
{
    public static class DataHolder
    {
        public static Dictionary<string, object> Data { get; set; }
        public static string SelectedKey { get; set; }
        public static Object SelectedObject
        {
            get
            {
                return Data[SelectedKey];
            }
        }

        public static void CreateTestCustomer ()
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

        static DataHolder()
        {
            Data = new Dictionary<string, object>();
        }

        internal static void CreateTestPerson()
        {
            Person newPerson = new Person();
            newPerson.FirstName = "Zorica";
            newPerson.LastName = "Banjac";

            Data.Add("TestPerson", newPerson);
            SelectedKey = "TestPerson";
        }
    }
}
