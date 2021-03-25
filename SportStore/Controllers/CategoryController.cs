using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportStore.Application;
using SportStore.Application.Categories.Commands;
using SportStore.Application.Categories.Queries;
using System.Threading.Tasks;

namespace SportStore.WebUi.Controllers
{
    public class CategoryController : ControllerBase
    {
        public CategoryController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize]
        public async Task<IActionResult> CategoryList()
        {            
            return View(await Mediator.Handle(new GetAllCategories()));
        }
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public IActionResult Add() => View(new AddCategoryCommand());
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var category = await Mediator.Handle(new GetCategoryById { Id = categoryId });
            if (category is null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete"), Authorize]
        public async Task<IActionResult> DeleteConfirmed(DeleteCategory command)
        {
            await Mediator.Handle(command);
            return RedirectToAction("CategoryList");
        }
    }
}