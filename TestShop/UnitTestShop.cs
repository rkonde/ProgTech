using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicLayer;
using DataLayer;
using System.Diagnostics;

namespace TestShop
{
    [TestClass]
    public class UnitTestShop
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

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
            DataGeneration datagen = new DataGeneration();
            shopLogic.ParseData(datagen.GiveData());

            Assert.IsTrue(shopLogic.IsInStock("Chocolate"));
            Assert.IsTrue(shopLogic.IsInStock("Bun"));
            Assert.IsTrue(shopLogic.IsInStock("Cheese"));
            Assert.IsTrue(shopLogic.IsInCatalog("Eggs"));
            Assert.IsTrue(shopLogic.IsInCatalog("Water"));
            Assert.IsTrue(shopLogic.IsInCatalog("Milk"));
            Assert.IsTrue(shopLogic.IsInShop("Mark"));
            Assert.IsTrue(shopLogic.IsInShop("Cody"));
            Assert.IsTrue(shopLogic.IsInShop("Anna"));

            shopLogic.AddToBasket(shopLogic.shop.Clients[1], shopLogic.shop.Stock[0]);
            shopLogic.AddToBasket(shopLogic.shop.Clients[1], shopLogic.shop.Stock[4]);

            Assert.IsTrue(shopLogic.ValueOfBasket(shopLogic.shop.Clients[1]) == 5.20);
            Assert.IsFalse(shopLogic.Checkout(shopLogic.shop.Clients[1]));

            shopLogic.RemoveFromBasket(shopLogic.shop.Clients[1], shopLogic.shop.Clients[1].Basket[1]);
            shopLogic.AddToBasket(shopLogic.shop.Clients[1], shopLogic.shop.Stock[1]);
            
            Assert.IsTrue(shopLogic.Checkout(shopLogic.shop.Clients[1]));
            Assert.IsTrue(shopLogic.IsInStock("Ice Cream"));
            Assert.IsFalse(shopLogic.IsInStock("Water"));
        }
    }
}
