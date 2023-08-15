using LazyCache;
using MediatR;
using ProductsManagement.AppDbContext;

namespace ProductsManagement.Features.Category.Commands
{
    public record AddCategoryCommand(CategoryModel Category) : IRequest<CategoryModel>;
    public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, CategoryModel>
    {
        private readonly AppDBContext _context;
        private readonly IAppCache _cache;
        public AddCategoryHandler(AppDBContext context,IAppCache cache)
        {
            _context = context;
            _cache = cache;
        }
        public async Task<CategoryModel> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            CategoryModel newCategory = request.Category;
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            _cache.Remove("AllCategories");
            return newCategory;
        }
    }
}