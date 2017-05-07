using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingReflaction.TestEntities
{
    public class Order
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public double Amount { get; set; }

        public Order(int id, string name, double amount)
        {
            Id = id;
            ProductName = name;
            Amount = amount;
        }
    }
}
