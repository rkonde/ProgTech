using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace TestShop
{
    class DataGeneration
    {
        private Shop shop;

        public DataGeneration()
        {
            shop = new Shop();
        }

        public Client client1 = new Client(5.0) { Name = "Mark" };
        public Client client2 = new Client(5.0) { Name = "Cody" };
        public Client client3 = new Client(5.0) { Name = "Anna" };
        public Client client4 = new Client(5.0) { Name = "Sarah" };
        public Client client5 = new Client(5.0) { Name = "Gabe" };
        private List<Client> Clients;

        public Product product1 = new Product(1.45, "Chocolate");
        public Product product2 = new Product(0.70, "Milk");
        public Product product3 = new Product(0.30, "Water");
        public Product product4 = new Product(0.20, "Bun");
        public Product product5 = new Product(2.35, "Cereal");
        public Product product6 = new Product(3.75, "Ice Cream");
        public Product product7 = new Product(1.65, "Cheese");
        public Product product8 = new Product(0.95, "Ketchup");
        public Product product9 = new Product(1.75, "Eggs");
        public Product product10 = new Product(3.45, "Pizza");
        private List<Product> Products;

        public Shop GiveData()
        {
            Clients = new List<Client> { client1, client2, client3, client4, client5 };
            Products = new List<Product> { product1, product2, product3, product4, product5, product6, product7, product8, product9, product10 };
            shop.Catalog = Products;
            shop.Clients = Clients;
            shop.Stock = Products;
            return shop;
        }
    }
}
