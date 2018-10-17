using DataLayer;
using System.Collections.Generic;
using System;

namespace LogicLayer
{


    public class ShopLogic
    {
        public Shop shop { get; }

        public ShopLogic()
        {
            shop = new Shop();
        }
    
        public bool AddToBasket(Client client, Product product)
        {
            if (IsInStock(product) && IsInShop(client))
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
            shop.Catalog.Add(product);
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
            double PriceOfProducts = 0;
            foreach (Product product in client.Basket)
            {
                PriceOfProducts += product.Price;
            }
            return PriceOfProducts;
        }

        public bool IsInStock(Product product)
        {
            return shop.Stock.Contains(product) ? true : false;
        }

        public bool IsInCatalog(Product product)
        {
            return shop.Catalog.Contains(product) ? true : false;
        }

        public bool IsInShop(Client client)
        {
            return shop.Clients.Contains(client) ? true : false;
        }
    }

}
