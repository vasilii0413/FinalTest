using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsManagement.AppDbContext;

namespace ProductsManagement.Features.WishList.Queries
{
    public class GetAllWishListsQuery : IRequest<List<WishListDto>> { }

    public class WishListDto
    {
        public string Username { get; set; }
        public string ProductName { get; set; }
        public int WishlistId { get; set; }
    }
    public class GetAllWishListsQueryHandler : IRequestHandler<GetAllWishListsQuery, List<WishListDto>>
    {
        private readonly AppDBContext _context;

        public GetAllWishListsQueryHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<WishListDto>> Handle(GetAllWishListsQuery request, CancellationToken cancellationToken)
        {
            var wishlistItems = await _context.WishLists
                .Include(item => item.User)
                .Include(item => item.Product)
                .ToListAsync();

            var wishlistItemDtos = wishlistItems.Select(item => new WishListDto
            {
                Username = item.User.UserName,
                ProductName = item.Product.ProductName,
                WishlistId = item.WishListId
            }).ToList();

            return wishlistItemDtos;
        }
    }

}
