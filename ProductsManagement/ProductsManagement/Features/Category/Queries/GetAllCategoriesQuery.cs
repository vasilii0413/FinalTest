using LazyCache;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsManagement.AppDbContext;

namespace ProductsManagement.Features.Category.Queries
{
    public record GetAllCategoriesQuery : IRequest<List<CategoryModel>>;
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryModel>>
    {
        private readonly AppDBContext _context;
        private readonly IAppCache _cache;
        public GetAllCategoriesHandler(AppDBContext context,IAppCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<CategoryModel>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            List<CategoryModel> categories = await _cache.GetOrAddAsync("AllCategories", 
                async () => await _context.Categories.ToListAsync()
            );
            return categories;
        }
    }
}