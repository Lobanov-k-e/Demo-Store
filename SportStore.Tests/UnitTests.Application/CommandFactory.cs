using SportStore.Application.Categories.Commands;
using SportStore.Application.Orders;
using SportStore.Application.Orders.Commands;
using SportStore.Application.Products.Queries;

namespace SportStore.UnitTests.UnitTests.Application
{
    class CommandFactory
    {
        internal static CreateNewOrder GetCreateNewOrderCommand(OrderVm order)
        {
            return new CreateNewOrder() { Order = order};
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

    }
}
