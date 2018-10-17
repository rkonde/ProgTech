using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class Product : IEquatable<Product>
    {
        public Product(double price, string name)
        {
            Price = price;
            Name = name;
        }

        public double Price;
        public string Name;
        public string Id;

        public bool Equals(Product product)
        {
            return product.Name == Name && product.Price == Price;
        }
    }
}
