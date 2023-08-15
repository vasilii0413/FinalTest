using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductsManagement.Features.Category.Queries;
using ProductsManagement.Features.Category.Commands;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace ProductsManagement.Features.Category
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AllowAnonymous]
        [HttpGet]
        [SwaggerOperation(
        Summary = "Show all categories",
        Description = "This API will show all categories",
        OperationId = "ShowCategories",
        Tags = new[] { "Categories" })]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(categories);
        }

        //[Authorize(Roles = "Manager")]
        [AllowAnonymous]
        [HttpPost]
        [SwaggerOperation(
        Summary = "Add a category",
        Description = "This API will add a new category",
        OperationId = "AddCategory",
        Tags = new[] { "Categories" })]
        public async Task<IActionResult> AddCategory(CategoryModel category)
        {
            try
            {
                await _mediator.Send(new AddCategoryCommand(category));
                return Ok(category);

            }
            catch
            {
                return NotFound();
            }
        }
    }
}
