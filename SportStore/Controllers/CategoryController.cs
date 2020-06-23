using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStore.Application;
using SportStore.Application.Categories.Commands;
using SportStore.Application.Categories.Queries;

namespace SportStore.WebUi.Controllers
{
    public class CategoryController : ControllerBase
    {
        public CategoryController(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IActionResult> CategoryList()
        {            
            return View(await Mediator.Handle(new GetAllCategories()));
        }

        public async Task<IActionResult> Edit(int categoryId)        
        {
            var category = await Mediator.Handle(new GetCategoryById() { Id = categoryId });
            if (category == null)
            {
                //should return view with error details
                return NotFound();
            }
            return View(new EditCategory(category));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditCategory command)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Handle(command);
                return RedirectToAction(nameof(CategoryList));
            }
            return View(command);           
        }

        public IActionResult Add() => View(new AddCategoryCommand());

        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryCommand command)
        {
            if (ModelState.IsValid)
            {
                await Mediator.Handle(command);
                return RedirectToAction(nameof(CategoryList));
            }
            return View(command);
        }
    }
}