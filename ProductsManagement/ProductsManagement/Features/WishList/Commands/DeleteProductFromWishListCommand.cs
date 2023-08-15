using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsManagement.AppDbContext;

public class DeleteProductFromWishlistCommand : IRequest<bool>
{
    public int ProductId { get; set; } 
    public string UserId { get; set; }
}

public class DeleteProductFromWishlistCommandHandler : IRequestHandler<DeleteProductFromWishlistCommand, bool>
{
    private readonly AppDBContext _context;

    public DeleteProductFromWishlistCommandHandler(AppDBContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteProductFromWishlistCommand request, CancellationToken cancellationToken)
    {
        var wishlistItem = await _context.WishLists
            .FirstOrDefaultAsync(item => item.Product.ProductId== request.ProductId && item.UserId == request.UserId);

        if (wishlistItem == null)
        {
            return false;
        }

        _context.WishLists.Remove(wishlistItem);
        await _context.SaveChangesAsync();

        return true; 
    }
}
