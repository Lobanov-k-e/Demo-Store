using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using SportStore.WebUi.Common;
using SportStore.Application.Products.Queries;
using System.Linq;

namespace SportStore.UnitTests.UnitTests.WebUi
{
    [TestFixture]
    class CartTests
    {
        [Test]
        public void AddItem_AddsNewItem()
        {
            var cart = new Cart();
            var product = new ProductDTO() { Id = 1 };
            cart.AddItem(product, 1);
            var lines = cart.GetLines();
            Assert.IsTrue(lines.Count() == 1);
            Assert.IsTrue(lines.First().Product == product);
        }
        [Test]
        public void AddItem_MultipleItems()
        {
            var cart = new Cart();
            var product0 = new ProductDTO() { Id = 1 };
            var product1 = new ProductDTO() { Id = 2 };
            var product2 = new ProductDTO() { Id = 3 };
            cart.AddItem(product0, 1);
            cart.AddItem(product1, 44);
            cart.AddItem(product2, 5);
            var lines = cart.GetLines().ToList();
            Assert.IsTrue(lines.Count() == 3);
            Assert.IsTrue(lines[0].Product == product0);
            Assert.IsTrue(lines[1].Product == product1);
            Assert.IsTrue(lines[2].Product == product2);
        }

        [Test]
        public void AddItem_AddsInitialQuantity()
        {
            var cart = new Cart();
            var product = new ProductDTO() { Id = 1 };
            const int Quantity = 10;
            cart.AddItem(product, Quantity);
            var lines = cart.GetLines();
            Assert.AreEqual(lines.First().Quantity, Quantity);
        }

        [Test]
        public void AddItem_AddsQuantity()
        {
            var cart = new Cart();
            var product = new ProductDTO() { Id = 1 };
            var product1 = new ProductDTO() { Id = 2 };
            int Quantity = 10;
            cart.AddItem(product, Quantity);
            cart.AddItem(product1, 1);
            cart.AddItem(product, Quantity);

            var lines = cart.GetLines().ToList();

            Assert.AreEqual(lines[0].Quantity, Quantity * 2);
            Assert.AreEqual(lines[1].Quantity, 1);
        }

        [Test]
        public void CanRemove()
        {
            var cart = new Cart();
            var product0 = new ProductDTO() { Id = 1 };
            var product1 = new ProductDTO() { Id = 2 };
            var product2 = new ProductDTO() { Id = 3 };
            cart.AddItem(product0, 1);
            cart.AddItem(product1, 44);
            cart.AddItem(product2, 5);

            cart.RemoveItem(product1);
            var lines = cart.GetLines();
            Assert.IsTrue(lines.Count() == 2);
            Assert.IsFalse(lines.Any(l => l.Product == product1));
        }

        [Test]
        public void CanClear()
        {
            var cart = new Cart();
            var product0 = new ProductDTO() { Id = 1 };
            var product1 = new ProductDTO() { Id = 2 };
            var product2 = new ProductDTO() { Id = 3 };
            cart.AddItem(product0, 1);
            cart.AddItem(product1, 44);
            cart.AddItem(product2, 5);

            cart.Clear();

            Assert.AreEqual(0, cart.GetLines().Count());
        }

        [Test]
        public void GetLines_GetsAllLines()
        {
            var cart = new Cart();
            int expectedCount = 50;
            for (int i = 0; i < expectedCount; i++)
            {
                cart.AddItem(new ProductDTO() { Id = i }, 1);
            }
            var lines = cart.GetLines();
            Assert.IsTrue(lines.Count() == expectedCount);
        }      


        [Test]
        public void GetsAccurate_TotalSumm()
        {
            var cart = new Cart();
            int expectedTotal = 0;
            for (int i = 0; i < 20; i++)
            {
                cart.AddItem(new ProductDTO() { Id = i, Price = i }, 1);
                expectedTotal += i;
            }

            Assert.AreEqual(expectedTotal, cart.CalculateSumm());            
        }
       

    }
}
