using Microsoft.AspNetCore.Mvc;
using SportStore.Application;
using SportStore.Application.Categories.Queries;
using SportStore.Application.Products.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.WebUi.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public NavigationMenuViewComponent(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentCategory = RouteData?.Values["currentCategory"] as string;
            var result = await _mediator.Handle(new GetCategoiesNavQuery() { CurrentCategoryName = currentCategory});

            return View(result);
        }
    }
}
