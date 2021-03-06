using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Application.Products.Queries;
using System;
using System.Collections.Generic;

namespace SportStore.WebUi.Common
{
    /// <summary>
    ///Cart decorator. Adds session storage
    /// </summary>
    public class SessionCart: ICart
    {
        private const string SerializationName = "Cart";
        private readonly ICart _cart;
        private readonly ISession _session;

        private SessionCart(ICart cart, ISession session)
        {
            _cart = cart ?? throw new ArgumentNullException(nameof(cart));
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }
        public static ICart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            ICart cart = new SessionCart(session.GetJson<Cart>(SerializationName) ?? new Cart(), session);            
            return cart;
        }

        private void SaveChanges()
        {
            _session.SetJson(SerializationName, _cart);
        }

        public IEnumerable<CartLine> Lines => _cart.Lines;

        public int TotalItems => _cart.TotalItems;

        public void AddItem(ProductDTO product, int quantity)
        {
            _cart.AddItem(product, quantity);
            SaveChanges();
        }        

        public void Clear()
        {
            _cart.Clear();
            _session.Remove(SerializationName);
        }

        public IEnumerable<CartLine> GetLines() => _cart.Lines;       

        public void RemoveItem(ProductDTO product)
        {
            _cart.RemoveItem(product);
            SaveChanges();
        }
        public decimal CalculateSumm() => _cart.CalculateSumm();
       
    }
}
