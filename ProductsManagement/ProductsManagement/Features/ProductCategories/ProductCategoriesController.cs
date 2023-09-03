using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsManagement.AppDbContext;
using ProductsManagement.Features.ProductCategories.Commands;
using ProductsManagement.Features.ProductCategories.Queries;
using ProductsManagement.Features.WishList;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductsManagement.Features.ProductCategories
{
    [Route("api/ProductCategories")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductCategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [SwaggerOperation(
         Summary = "Add product to category",
        Description = "This API allows adding a product to a category",
        OperationId = "AddProductToCategory",
        Tags = new[] { "ProductCategories" })]
        public async Task<IActionResult> AddProductToCategory(byte categoryId, short productId)
        {
            var command = new AddProductToCategoryCommand
            {
                CategoryId = categoryId,
                ProductId = productId
            };

            var success = await _mediator.Send(command);

            if (success)
            {
                return Ok("Product added to category.");
            }
            else
            {
                return NotFound("Category or product not found.");
            }
        }

            [AllowAnonymous]
            [HttpGet("GetAll")]
            [SwaggerOperation(
            Summary = "Get all product-category entries",
            Description = "This API retrieves all entries from the product-category bridge table",
            OperationId = "GetAllProductCategory",
            Tags = new[] { "ProductCategories" })]
            [ProducesResponseType(200,Type = typeof(IEnumerable<ProductCategory>))]
        public async Task<IActionResult> GetAllProductCategory()
        {
            var query = new GetAllProductCategoryQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
