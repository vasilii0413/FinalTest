using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsManagement.AppDbContext;
using System.Linq;

namespace ProductsManagement.Features.Product.Queries
{
    public record GetAllProductsQuery : IRequest<List<ProductModel>>;
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<ProductModel>>
    {
        private readonly AppDBContext _context;
        public GetAllProductsHandler(AppDBContext context) => _context = context;

        public async Task<List<ProductModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            List<ProductModel> Products = await _context.Products.ToListAsync();
            return Products;
        }
    }
}