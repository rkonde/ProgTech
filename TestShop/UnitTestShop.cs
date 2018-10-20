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
            DataGeneration datagen = new DataGeneration();
            //shopLogic.shop = datagen.GiveData();
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

            shopLogic.AddToBasket(datagen.client2, datagen.product1);
            shopLogic.AddToBasket(datagen.client2, datagen.product6);
            Assert.IsFalse(shopLogic.Checkout(datagen.client2));

            shopLogic.RemoveFromBasket(datagen.client2, datagen.product1);
            shopLogic.AddToBasket(datagen.client2, datagen.product2);
            

            Assert.IsTrue(shopLogic.Checkout(datagen.client2));
            //Assert.IsTrue(datagen.client2.Money == 0.55);
            Assert.IsFalse(shopLogic.IsInStock("Ice Cream"));
            Assert.IsFalse(shopLogic.IsInStock("Milk"));
            //Assert.IsTrue(shopLogic.IsInCatalog("Ice Cream"));
            //Assert.IsTrue(shopLogic.IsInCatalog("Milk"));
        }
    }
}
