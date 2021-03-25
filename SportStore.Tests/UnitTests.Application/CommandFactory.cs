using SportStore.Application.Categories.Commands;
using SportStore.Application.Orders;
using SportStore.Application.Orders.Commands;
using SportStore.Application.Products.Commands;
using SportStore.Application.Products.Queries;
using System;

namespace SportStore.UnitTests.UnitTests.Application
{
    class CommandFactory
    {
        internal static CreateNewOrder GetCreateNewOrderCommand(OrderVm order)
        {
            return new CreateNewOrder() { Order = order};
        }

        internal static EditOrderCommand EditOrder(int id, bool isGiftWrap, bool shipped, CustomerVM customer)
        {
            return new EditOrderCommand() { OrderId = id, Customer = customer, IsGiftWrap = isGiftWrap, Shipped = shipped };
        }

        internal static AddCategoryCommand GetAddCategoryCommand(string name, string description)
        {
            return new AddCategoryCommand { Name = name, Description = description };
        }

        internal static EditCategory GetEditCategoryCommand(int id, string name, string description)
        {
            return new EditCategory() { Id = id, Name = name, Description = description };
        }

        internal static EditCategory GetEditCategoryCommand(CategoryDTO category)
        {
            return new EditCategory(category);
        }

        internal static DeleteCategory GetDeleteCategoryCommand(int id)
        {
            return new DeleteCategory() { Id = id };
        }

        internal static AddProductCommand AddProduct(string name, string description, int catIt, decimal price)
        {
            return new AddProductCommand()
            {
                Name = name,
                Description = description,
                CategoryId = catIt,
                Price = price
            };
        }

        internal static EditProductCommand EditProductCommand(int id, string name, string description, int catIt, decimal price)
        {
            return new EditProductCommand()
            {
                Id = id,
                Name = name,
                Description = description,
                CategoryId = catIt,
                Price = price
            };
        }

        internal static DeleteProductCommand DeleteProduct(int id)
        {
            return new DeleteProductCommand() { ProductId = id };
        }

        internal static DeleteOrderCommand DeleteOrder(int id)
        {
            return new DeleteOrderCommand() { OrderId = id };
        }

        internal static ShipOrderCommand ShipOrderCommand(int id)
        {
            return new ShipOrderCommand() { OrderId = id };
        }
    }
}
