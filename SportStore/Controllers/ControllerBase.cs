using Microsoft.AspNetCore.Mvc;
using SportStore.Application;

namespace SportStore.WebUi.Controllers
{
    public class ControllerBase : Controller
    {
        private readonly IMediator _mediator;

        public ControllerBase(IMediator mediator)
        {
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
        }

        protected IMediator Mediator => _mediator;
    }
}
