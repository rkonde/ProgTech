using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Client
    {
        public Client(double money)
        {
            Basket = new List<Product>();
            Money = money;
        }

        public string Name { get; set; }
        public ICollection<Product> Basket { get; set; }
        public double Money;
    }
}
