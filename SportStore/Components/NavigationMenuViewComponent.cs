using Microsoft.AspNetCore.Mvc;
using SportStore.Application;
using SportStore.Application.Categories.Queries;
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
            var result = await _mediator.Handle(new GetAllCategoriesQuery());
            return View(result);
        }
    }
}
