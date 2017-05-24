using System.Collections.Generic;

namespace UsingReflection.TestEntities
{
    public class Customer : Person
    {
        public int CustomerId { get; set; }
        public string EmailAddress { get; set; }
        public bool IsPrivileged { get; set; }

        public Address MailingAddress { get; set; }
        public bool IsSolvent { get; set; }

        public List<Order> Orders = new List<Order>();

        public Customer() { }

        public Customer(int customerId, string firstName, string lastName, string emailAddress, bool isSolvent, bool isPrivileged)
        {
            CustomerId = customerId;

            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            IsSolvent = isSolvent;
            IsPrivileged = isPrivileged;
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
