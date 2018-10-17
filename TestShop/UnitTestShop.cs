using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataLayer;

namespace TestShop
{
    [TestClass]
    public class UnitTestShop
    {
        [TestMethod]
        public void TestProduct()
        {
            Product milk = new Product(3.5, "milk");
            Product bun = new Product(0.65, "bun");

            Assert.AreEqual(milk.Price, 3.5);
            Assert.AreEqual(milk.Name, "milk");
            Assert.AreEqual(bun.Price, 0.65);
            Assert.AreEqual(bun.Name, "bun");

        }

        [TestMethod]
        public void TestClient()
        {
            Client client = new Client(345.76);
            client.Name = "mark";
            Assert.AreEqual(client.Money, 345.76);
            Assert.AreEqual(client.Name, "mark");
        }

        [TestMethod]
        public void TestEvent()
        {
            Product milk = new Product(3.5, "milk");
            Product bun = new Product(0.65, "bun");
            Client client = new Client(345.76);

            client.Basket.Add(milk);
            client.Basket.Add(bun);

            Invoice invoice = new Invoice();
            invoice.ListOfGoods = client.Basket;
            Event event1 = new Event("Checkout");
            event1.Invoice = invoice;

            Assert.AreEqual(event1.Name,"Checkout");
            Assert.AreEqual(event1.Invoice, invoice);
            Assert.AreEqual(event1.Invoice.ListOfGoods, invoice.ListOfGoods);
            Assert.AreEqual(event1.Invoice.ListOfGoods, client.Basket);
        }

        [TestMethod]
        public void TestInvoice()
        {
            Product milk = new Product(3.5, "milk");
            Product bun = new Product(0.65, "bun");
            Client client = new Client(345.76);

            client.Basket.Add(milk);
            client.Basket.Add(bun);

            Invoice invoice = new Invoice();
            invoice.ListOfGoods = client.Basket;

            Assert.AreEqual(invoice.ListOfGoods, client.Basket);
        }

        [TestMethod]
        public void TestLogic()
        {
            ShopLogic shopLogic = new ShopLogic();
            Client client1 = new Client(5)
            {
                Name = "Mark"
            };
            Client client2 = new Client(4)
            {
                Name = "Zuckerberg"
            };
            Product milk = new Product(3.5, "milk");
            Product bun = new Product(1, "bun");

            shopLogic.EnterShop(client1);
            shopLogic.AddToCatalog(milk);
            shopLogic.AddToStock(milk);
            shopLogic.AddToCatalog(bun);
            shopLogic.AddToStock(bun);

            Assert.IsTrue(shopLogic.IsInStock(milk));
            Assert.IsTrue(shopLogic.IsInStock(bun));
            Assert.IsTrue(shopLogic.IsInCatalog(milk));
            Assert.IsTrue(shopLogic.IsInCatalog(bun));
            Assert.IsFalse(shopLogic.AddToBasket(client2, milk));

            shopLogic.EnterShop(client2);
            shopLogic.AddToBasket(client2, milk);
            shopLogic.AddToBasket(client2, bun);

            Assert.IsFalse(shopLogic.Checkout(client2));

            shopLogic.RemoveFromBasket(client2, bun);
            shopLogic.AddToBasket(client1, bun);

            Assert.IsTrue(shopLogic.Checkout(client2));
            Assert.IsTrue(shopLogic.Checkout(client1));
            Assert.AreEqual(client1.Money, 4);
            Assert.AreEqual(client2.Money, 0.5);
            Assert.IsFalse(shopLogic.IsInStock(milk));
            Assert.IsFalse(shopLogic.IsInStock(bun));
            Assert.IsTrue(shopLogic.IsInCatalog(milk));
            Assert.IsTrue(shopLogic.IsInCatalog(bun));
        }
    }
}
