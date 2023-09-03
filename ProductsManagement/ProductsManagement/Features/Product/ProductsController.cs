using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsManagement.Features.Product.Commands;
using ProductsManagement.Features.Product.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace ProductsManagement.Features.Product
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
            
        }
        [AllowAnonymous]
        [HttpGet]
        [SwaggerOperation(
        Summary = "Show products",
        Description = "This API will show all products",
        OperationId = "ShowProducts",
        Tags = new[] { "Products" })]
        [ProducesResponseType(200,Type = typeof(IEnumerable<ProductModel>))]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [SwaggerOperation(
        Summary = "Show specific product",
        Description = "This API will show an specific product by given id",
        OperationId = "ShowProduct",
        Tags = new[] { "Products" })]
        [ProducesResponseType(200,Type = typeof(ProductModel))]
        public async Task<ActionResult<ProductModel>> GetProduct(short id)
        {
            try
            {
                var product = await _mediator.Send(new GetSingleProductQuery(id));

                if (product == null)
                {
                    return NotFound();
                }

                return product;
            }
            catch
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Manager")]
        [Authorize]
        [HttpPost("add", Name = "AddNewProduct")]
        [SwaggerOperation(
        Summary = "Add Product",
        Description = "This API will add a new product",
        OperationId = "AddNewProduct",
        Tags = new[] { "Products" })]

        public async Task<IActionResult>AddProduct(ProductModel product)
        {
            try
            {
                await _mediator.Send(new AddProductCommand(product));
                return Ok(product);

            }
            catch
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Manager")]
        [HttpPut("{id}")]
        [SwaggerOperation(
        Summary = "Edit product",
        Description = "This API will edit an product by given id",
        OperationId = "EditProduct",
        Tags = new[] { "Products" })]
        public async Task<IActionResult> EditProduct(short? id, ProductModel updatedProduct)
        {
            try
            {
                if (id == null || id != updatedProduct.ProductId)
                {
                    return BadRequest("Invalid product ID.");
                }

                var result = await _mediator.Send(new UpdateProductCommand(updatedProduct));

                if (result == null)
                {
                    return NotFound("Product not found.");
                }

                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
        Summary = "Delete product",
        Description = "This API will delete an product by given id",
        OperationId = "DeleteProduct",
        Tags = new[] { "Products" })]
        public async Task<IActionResult> DeleteProduct(short? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("Invalid product ID.");
                }

                var deletedProduct = await _mediator.Send(new DeleteProductCommand(new ProductModel { ProductId = id.Value }));

                if (deletedProduct == null)
                {
                    return NotFound("Product not found.");
                }

                return NoContent(); 
            }
            catch 
            {
                return NotFound();
            }
        }
    }
}
