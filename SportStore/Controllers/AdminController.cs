using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportStore.Application;

namespace SportStore.WebUi.Controllers
{
    [Authorize]
    public class AdminController : ControllerBase
    {
        public AdminController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> SeedData()
        {
            var command = new SeedDataCommand();
            await Mediator.Handle(command);
            return Redirect("/Admin");
        }
    }
}