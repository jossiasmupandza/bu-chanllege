using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Categories;
using Application.Solutions;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoriesController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(CreateCategory.CreateCategoryCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet]
        public async Task<List<Category>> GetCategories()
        {
            return await Mediator.Send(new GetCategories.GetCategoriesQuery());
        }
    }
}