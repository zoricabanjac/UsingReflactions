using System.Collections.Generic;

namespace UsingReflection.TestEntities
{
    public class Customer : Person
    {
        public struct Address
        {
            public string Street, City, State, Zip;
            public override string ToString()
            {
                return Street + ", " + City + ", " + State + ", " + Zip;
            }
        }

        public bool isPrivileged;

        public bool isSolvent{get; set; }
        public int CustomerId { get; set; }
        public string EmailAddress { get; set; }
        public Address MailingAddress { get; set; }
        public List<Order> Orders = new List<Order>();
  
        public Customer() { }

        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Customer(string firstName, string lastName, params Order[] orders)
        {
            FirstName = firstName;
            LastName = lastName;
            foreach (Order order in orders) Orders.Add(order);
        }

        public delegate void PaymentReceivedDelegate(decimal amount);
        public event PaymentReceivedDelegate PaymentReceived;

        public string SendEmail(string message) 
        {
            return message + " sent"; 
        }

        public string SendMessage(string message, int numberOfReceivers, bool isSent)
        {
            return message + " sent to " + numberOfReceivers + "receivers " + isSent;
        }
    }
}
