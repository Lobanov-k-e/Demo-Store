
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NUnit.Framework;
using SportStore;
using SportStore.Application.Products.Queries;

namespace SportsStore.IntegrationTests
{
    [TestFixture]
    class ProductControllerTests
    {
        private CustomWebApplicationFactory<Startup> _factory;

        [OneTimeSetUp]
        public void SetUp()
        {
            _factory = new CustomWebApplicationFactory<Startup>();
        }

        [Test]
        public async Task GetProductPage_ReturnsPage()
        {
            var webApp = _factory.CreateClient();

            var response = await webApp.GetAsync($"/product/products");

            Assert.IsTrue(response.IsSuccessStatusCode);                  
        }
       


    }
}
