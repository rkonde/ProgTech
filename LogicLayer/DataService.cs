﻿using DataLayer;
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
                RemoveFromStock(product);
            }
        }

        public void RemoveFromBasket(Client client, Product product)
        {
            client.Basket.Remove(product);
            AddToStock(product);
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

        public Boolean Checkout(Client client)
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
    }

}
