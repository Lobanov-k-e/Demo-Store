using Microsoft.AspNetCore.Mvc;
using SportStore.WebUi.Controllers.ViewModels;
using System;
using System.Threading.Tasks;

namespace SportStore.WebUi.Controllers
{
    public class CartController : Controller
    {

        public async Task<IActionResult> AddToCart(AddToCartVm model)
        {
            int id = model.ProductId;
            //get post requst from addcart
            //get product by id
            //add item to cart
            //return redirect result
            throw new NotImplementedException();
        }
    }
}
