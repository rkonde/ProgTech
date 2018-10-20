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

        public List<Client> Clients { get; set; }
        public List<Product> Catalog { get; set; }
        public List<Event> Events { get; set; }
        public List<Product> Stock { get; set; }
    }
}
