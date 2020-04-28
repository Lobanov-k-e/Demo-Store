using SportStore.Application.Products.Queries;
using System.Collections.Generic;

namespace SportStore.WebUi.Common
{
    public interface ICart
    {
        IEnumerable<CartLine> Lines { get; }

        void AddItem(ProductDTO product, int quantity);
        void Clear();
        IEnumerable<CartLine> GetLines();
        void RemoveItem(ProductDTO product);
        decimal CalculateSumm();
        int TotalItems { get;}
    }
}