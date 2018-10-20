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

        public string Name { get; set; }
        public List<Product> Basket { get; set; }
        public double Money;

        public bool Equals(Client client)
        {
            return client.Name == Name;
        }
    }
}
