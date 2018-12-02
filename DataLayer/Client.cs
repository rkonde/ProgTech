using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Client : IEquatable<Client>
    {
        public Client(double money)
        {
            Basket = new List<Product>();
            Money = money;
        }

        public Client(double money, String name)
        {
            Basket = new List<Product>();
            Money = money;
            Name = name;
        }

        public string Name { get; set; }
        public List<Product> Basket { get; set; }
        public double Money;

        public bool Equals(Client client)
        {
            return client.Name == Name;
        }
    }
}
