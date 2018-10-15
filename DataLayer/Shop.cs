using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer
{
    public class Shop
    {
        public Shop()
        {
            Clients = new List<Client>();
            Catalog = new List<Product>();
            Events = new List<Event>();
            Stock = new List<Product>();
        }

        public ICollection<Client> Clients { get; set; }
        public ICollection<Product> Catalog { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Product> Stock { get; set; }
    }
}
