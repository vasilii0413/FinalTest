using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsManagement.AppDbContext;

namespace ProductsManagement.Features.Product.Queries
{
    public record GetSingleProductQuery(short ProductId) : IRequest<ProductModel>;

    public class GetSingleProductHandler : IRequestHandler<GetSingleProductQuery, ProductModel>
    {
        private readonly AppDBContext _context;
        public GetSingleProductHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<ProductModel> Handle(GetSingleProductQuery request, CancellationToken cancellationToken)
        {
            ProductModel product = await _context.Products
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(r => r.ProductId == request.ProductId);
            return product;
        }
    }
}