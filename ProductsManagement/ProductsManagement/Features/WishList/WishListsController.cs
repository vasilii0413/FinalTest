using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsManagement.AppDbContext;
using ProductsManagement.Features.WishList.Commands;
using ProductsManagement.Features.WishList.Queries;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace ProductsManagement.Features.WishList
{
    [Route("api/WishLists")]
    [ApiController]
    public class WishListsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AppDBContext _context;
        private UserManager<IdentityUser> _userManager;
        

        public WishListsController(IMediator mediator, UserManager<IdentityUser> userManager,AppDBContext context)
        {
            _mediator = mediator;
            _userManager = userManager;
            _context = context;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        [SwaggerOperation(
        Summary = "Show all wishlists",
        Description = "This API will show all wishlists",
        OperationId = "ShowAllWishLists",
        Tags = new[] { "WishLists" })]
        [ProducesResponseType(200,Type = typeof(IEnumerable<WishListModel>))]
        public async Task<IActionResult> GetAllWishLists()
        {
            var query = new GetAllWishListsQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

       
        [Authorize]
        [HttpGet("Authorized")]
        [SwaggerOperation(
        Summary = "Show wishlist of authorized user",
        Description = "This API will show the wishlist of the authorized user",
        OperationId = "ShowUserWishlist",
        Tags = new[] { "WishLists" })]
        [ProducesResponseType(200,Type = typeof(WishListModel))]
        public async Task<IActionResult> GetWishlist()
        {
            var userId = _userManager.GetUserId(User);
            //var userId = "da9cade9-5b6d-4533-b0a0-174faa8c73d1";

            var wishlistItems = await _context.WishLists
                .Where(item => item.UserId == userId)
                .Include(item => item.User)
                .Include(item => item.Product)
                .ToListAsync();

            var wishlistItemDtos = wishlistItems.Select(item => new WishListDto
            {
                Username = item.User.UserName,
                ProductName = item.Product.ProductName,
                WishlistId = item.WishListId
            }).ToList();

            return Ok(wishlistItemDtos);
        }

        [Authorize]
        [HttpPost("{productId}")]
        [SwaggerOperation(
        Summary = "Add a product to the wishlist",
        Description = "This API allows users to add a product to their wishlist",
        OperationId = "AddProductToWishList",
        Tags = new[] { "WishLists" })]
        public async Task<IActionResult> AddProductToWishList(short productId)
        {
            //var userId = "da9cade9-5b6d-4533-b0a0-174faa8c73d1";
            var userId = _userManager.GetUserId(User);
            var command = new AddProductToWishListCommand(productId, userId);
            var success = await _mediator.Send(command);
            
            if (success)
            {
                return Ok("Product added to wishlist.");
            }
            else
            {
                return BadRequest("Failed to add product to wishlist.");
            }
        }

        [Authorize]
        [HttpDelete("DeleteFromWishlist")]
        [SwaggerOperation(
        Summary = "Delete a product from wishlist",
        Description = "This API allows deleting a product from the authorized user's wishlist",
        OperationId = "DeleteProductFromWishlist",
        Tags = new[] { "WishLists" })]
        public async Task<IActionResult> DeleteProductFromWishlist([FromQuery] int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            //var userId = "da9cade9-5b6d-4533-b0a0-174faa8c73d1";
            var command = new DeleteProductFromWishlistCommand { ProductId = productId, UserId = userId };
            var success = await _mediator.Send(command);

            if (success)
            {
                return Ok("Product removed from wishlist.");
            }
            else
            {
                return NotFound("Product not found in wishlist.");
            }
        }


    }
}
