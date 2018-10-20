using DataLayer;
using System.Collections.Generic;
using System;

namespace LogicLayer
{


    public class ShopLogic
    {
        public Shop shop { get; set; }

        public ShopLogic()
        {
            shop = new Shop();
        }

        public void ParseData(Shop shop) => this.shop = shop;

        public bool AddToBasket(Client client, Product product)
        {
            if (IsInStock(product.Name) && IsInShop(client.Name))
            {
                client.Basket.Add(product);
                RemoveFromStock(product);
                return true;
            }
            else return false;
        }

        public void RemoveFromBasket(Client client, Product product)
        {
            client.Basket.Remove(product);
            AddToStock(product);
        }

        public void EnterShop(Client client)
        {
            shop.Clients.Add(client);
        }

        public void LeaveShopp(Client client)
        {
            shop.Clients.Remove(client);
        }

        public void AddToCatalog(Product product)
        {
            if (!IsInCatalog(product.Name))
            {
                shop.Catalog.Add(product);
            }
        }

        public void RemoveFromCatalog(Product product)
        {
            shop.Catalog.Remove(product);
        }

        public void AddToStock(Product product)
        {
            shop.Stock.Add(product);
        }

        public void RemoveFromStock(Product product)
        {
            shop.Stock.Remove(product);
        }

        public bool Checkout(Client client)
        {
            if (CanPay(client))
            {
                Event anEvent = new Event("Checkout of " + client.Name);
                Invoice anInvoice = new Invoice();
                anInvoice.ListOfGoods = client.Basket;
                anEvent.Invoice = anInvoice;
                shop.Events.Add(anEvent);
                Pay(client);
                client.Basket.Clear();
                return true;
            }
            else return false;
        }

        private void Pay(Client client)
        {
            client.Money -= ValueOfBasket(client);
        }

        private bool CanPay(Client client)
        {
            if (client.Money >= ValueOfBasket(client))
            {
                return true;
            }
            else return false;
        }

        private double ValueOfBasket(Client client)
        {
            double PriceOfProducts = 0.0;
            foreach (Product product in client.Basket)
            {
                PriceOfProducts += product.Price;
            }
            return PriceOfProducts;
        }

        public bool IsInStock(String productName)
        {
            Product temp = new Product(0.0, productName);
            return shop.Stock.Contains(temp) ? true : false;
        }

        public bool IsInCatalog(String productName)
        {
            Product temp = new Product(0.0, productName);
            return shop.Catalog.Contains(temp) ? true : false;
        }

        public bool IsInShop(String clientName)
        {
            Client temp = new Client(0.0) { Name = clientName };
            return shop.Clients.Contains(temp) ? true : false;
        }
    }

}
