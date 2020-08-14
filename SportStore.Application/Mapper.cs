using SportStore.Application.Interfaces;
using SportStore.Application.Orders;
using SportStore.Application.Products.Queries;
using SportStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SportStore.Application.Products
{
    public class Mapper : IMapper
    {
        public IEnumerable<ProductDTO> MapProductsToDTO(List<Product> products)
        {
            return (products.Select(product => MapProductToDTO(product))).ToList();
        }

        public ProductDTO MapProductToDTO(Product product)
        {
            //this is smelly
            if (product is null)
                return null;           

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Category = product.Category is null ? null : MapCategoryToDTO(product.Category)
            };
        }

        public CategoryDTO MapCategoryToDTO(Category category)
        {
            if (category is null)
                return null;

            return new CategoryDTO()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public Category MapCategoryToDomain(CategoryDTO category)
        {
            return new Category
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public IEnumerable<CategoryDTO> MapCategoriesToDTO(List<Category> categories)
        {
            return categories.Select(c => MapCategoryToDTO(c)).ToList();
        }

        public OrderVm MapOrderToVm(Order order)
        {
            if (order is null)
            {
                return null;
            }

            return new OrderVm
            {
                OrderId = order.Id,
                OrderLines = MapLines(order.OrderLines),
                Customer = CreateCustomerVm(order),
                GiftWrap = order.GiftWrap,
                Shipped = order.Shipped
            };
        }

        private CustomerVM CreateCustomerVm(Order order)
        {
            return new CustomerVM
            {
                Name = order.Name,
                Adress = MapAdressToVM(order.CustomerAdress)
            };
        }

        public AdressVm MapAdressToVM(Adress customerAdress)
        {
            return new AdressVm
            {
                Line1   = customerAdress.Line1,
                Line2   = customerAdress.Line2,
                Line3   = customerAdress.Line3,
                Country = customerAdress.Country,
                City    = customerAdress.City,
                State   = customerAdress.State,
                Zip     = customerAdress.Zip
            };
        }

        public Adress MapAdressToDomain(AdressVm vm)
        {
            return new Adress
            {
                Line1 = vm.Line1,
                Line2 = vm.Line2,
                Line3 = vm.Line3,
                Country = vm.Country,
                City = vm.City,
                State = vm.State,
                Zip = vm.Zip
            };
        }

        private ICollection<OrderLineVm> MapLines(ICollection<OrderLine> orderLines)
        {
            return orderLines.Select(l => MapLine(l)).ToList();
        }

        private OrderLineVm MapLine(OrderLine line)
        {
            return new OrderLineVm
            {
                Product = MapProductToDTO(line.Product),
                Quantity = line.Quantity
            };
        }

        public Order MapOrderVmToDomain(OrderVm orderVm)
        {
            var order = new Order()
            {
                Id = orderVm.OrderId,
                OrderLines = MapLineVmToLies(orderVm.OrderLines),
                Name = orderVm.Customer.Name,
                CustomerAdress = MapAdressToDomain(orderVm.Customer.Adress),
                GiftWrap = orderVm.GiftWrap,
                Shipped = orderVm.Shipped
            };
            return order;
        }

     

        private ICollection<OrderLine> MapLineVmToLies(IEnumerable<OrderLineVm> orderLines)
        {
            return orderLines.Select(l => MapLineToDomain(l)).ToList();
        }

        private OrderLine MapLineToDomain(OrderLineVm lineVm)
        {
            return new OrderLine()
            {
                Product = MapProductToDomain(lineVm.Product),
                Quantity = lineVm.Quantity
            };
        }

        public Product MapProductToDomain(ProductDTO dto)
        {
            return new Product()
            {
                Id = dto.Id,
                Name = dto.Name,
                CategoryId = dto.CategoryId,
                Description = dto.Description,
                Price = dto.Price
            };
        }

        public IEnumerable<OrderListItemDTO> MapOrdersToDTO(IEnumerable<Order> orders)
        {
            return orders.Select(o => new OrderListItemDTO() { Id = o.Id, Name = o.Name }).ToList();
        }
    }
}
