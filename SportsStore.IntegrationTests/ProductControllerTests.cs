
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

            var response = await webApp.GetAsync($"/product/productlist");

            GetResponseContent<ViewResult>(response);
            

            
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }


    }
}
