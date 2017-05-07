using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingReflaction.TestEntities
{
    public class Customer : Person
    {
        public struct Address
        {
            public string Street, City, State, Zip;
        }

        public string EmailAddress { get; set; }
        public Address MailingAddress { get; set; }
        public List<Order> Orders = new List<Order>();

        public Customer() { }
        public Customer(string firstName, string lastName, params Order[] orders)
        {
            FirstName = firstName;
            LastName = lastName;
            foreach (Order order in orders) Orders.Add(order);
        }

        public delegate void PaymentReceivedDelegate(decimal amount);
        public event PaymentReceivedDelegate PaymentReceived;

        public void SendEmail(string message) { }
        public void SendMessage(string message, int numberOfReceivers) { }
    }
}
