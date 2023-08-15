using MediatR;
using Microsoft.AspNetCore.Identity;
using ProductsManagement.AppDbContext;

namespace ProductsManagement.Features.WishList.Commands
{
    public record AddProductToWishListCommand(short ProductId, string UserId) : IRequest<bool>;
    public class AddProductToWishListHandler : IRequestHandler<AddProductToWishListCommand, bool>
    {
        private readonly AppDBContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AddProductToWishListHandler(AppDBContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> Handle(AddProductToWishListCommand request, CancellationToken cancellationToken)
        {
            
            var product = await _context.Products.FindAsync(request.ProductId);

            if (product == null)
            {
                
                return false;
            }
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                
                return false;
            }
            
            var wishListItem = new WishListModel
            {
                UserId = user.Id,
                Product = product
            };
            
            _context.WishLists.Add(wishListItem);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
