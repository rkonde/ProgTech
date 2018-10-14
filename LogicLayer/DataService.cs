using DataLayer;
using System.Collections.Generic;
using System;

namespace LogicLayer
{
    public class DataService
    {
        public Shop shop { get; }
    
        public void AddToBasket(Client client, Product product)
        {
            if (IsInStock(product))
            {
                client.Basket.Add(product);
                shop.InStock.Remove(product);
            }
        }

        public void AddToCatalog(Product product)
        {
            shop.Catalog.Add(product);
        }

        public void AddToStock(Product product)
        {
            shop.Catalog.Add(product);
        }

        public void Checkout(Client client)
        {
            if (CanTakeMoneyFrom(client))
            {
                Event anEvent = new Event("Checkout of " + client.Name);
                Invoice anInvoice = new Invoice();
                anInvoice.ListOfGoods = client.Basket;
                anEvent.Invoice = anInvoice;
                shop.Events.Add(anEvent);
                TakeMoneyFrom(client);
            }
            else TakeProductsFrom(client);
        }

        private void TakeMoneyFrom(Client client)
        {
            client.Money -= ValueOfBasket(client);
        }

        private void TakeProductsFrom(Client client)
        {
            foreach (Product product in client.Basket)
            {
                shop.InStock.Add(product);
            }
            client.Basket.Clear();
        }

        private bool CanTakeMoneyFrom(Client client)
        {
            if (client.Money > ValueOfBasket(client))
            {
                return true;
            }
            else return false;
        }

        private int ValueOfBasket(Client client)
        {
            int PriceOfProducts = 0;
            foreach (Product element in client.Basket)
            {
                PriceOfProducts += element.Price;
            }
            return PriceOfProducts;
        }

        public bool IsInStock(Product product)
        {
            return shop.InStock.Contains(product) ? true : false;
        }
    }

}
