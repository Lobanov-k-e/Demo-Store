using Newtonsoft.Json;
using SportStore.Application.Products.Queries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportStore.WebUi.Common
{

    public class Cart : ICart
    {
        private readonly LineCollection cartLines;

        public Cart()
        {
            cartLines = new LineCollection();
        }

        [JsonConstructor]
        public Cart(IEnumerable<CartLine> lines)
        {
            cartLines = new LineCollection();
            foreach (var line in lines)
            {
                cartLines.AddLine(line);
            }
        }
        public int TotalItems => cartLines.GetTotalItems();
        public IEnumerable<CartLine> Lines => GetLines();


        public void AddItem(ProductDTO product, int quantity)
        {
            _ = product ?? throw new ArgumentNullException("product should not be null");

            var line = cartLines.FindLine(product);
            if (line is null)
            {
                line = new CartLine()
                {
                    Product = product
                };
                cartLines.AddLine(line);
            }
            line.Quantity += quantity;
        }

        public void RemoveItem(ProductDTO product)
        {
            _ = product ?? throw new ArgumentNullException("product should not be null");
            cartLines.RemoveProduct(product);
        }

        public decimal CalculateSumm() => cartLines.CalculateTotal();

        public void Clear() => cartLines.Clear();

        public IEnumerable<CartLine> GetLines()
        {
            return cartLines.Items;
        }      
        
        private class LineCollection
        {
            private readonly List<CartLine> cartLines = new List<CartLine>();
            internal void AddLine(CartLine line) => cartLines.Add(line);
            internal void RemoveProduct(ProductDTO product) => cartLines.RemoveAll(l => l.Product.Id == product.Id);
            internal CartLine FindLine(ProductDTO product) => cartLines.Where(l => l.Product.Id == product.Id).SingleOrDefault();
            internal decimal CalculateTotal() => cartLines.Sum(l => l.Product.Price * l.Quantity);
            internal int GetTotalItems() => cartLines.Sum(l => l.Quantity);
            internal void Clear() => cartLines.Clear();
            public IEnumerable<CartLine> Items
            {
                get
                {
                    List<CartLine> result = new List<CartLine>(cartLines.Count());
                    result.AddRange(cartLines.Select(l => l.Copy()));
                    return result;
                }
            }
        }
    }

    public class CartLine
    {
        public int CartLineId { get; set; }
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }

        public decimal LinePrice { 
            get
            {
                return Product.Price * Quantity;
            } 
        }

        public CartLine Copy()
        {
            return new CartLine()
            {
                CartLineId = this.CartLineId,
                Product = this.Product,
                Quantity = this.Quantity
            };
        }
    }
}
