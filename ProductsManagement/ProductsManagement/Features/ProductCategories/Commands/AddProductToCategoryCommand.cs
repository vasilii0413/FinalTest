using MediatR;
using ProductsManagement.AppDbContext;
using System.ComponentModel.DataAnnotations;

namespace ProductsManagement.Features.ProductCategories.Commands
{
    public class AddProductToCategoryCommand : IRequest<bool>
    {        
        public byte CategoryId { get; set; }
        public short ProductId { get; set; }

        public class AddProductToCategoryCommandHandler : IRequestHandler<AddProductToCategoryCommand, bool>
        {
            private readonly AppDBContext _context;

            public AddProductToCategoryCommandHandler(AppDBContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(AddProductToCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = await _context.Categories.FindAsync(request.CategoryId);
                if (category == null)
                {
                    return false;
                }

                var product = await _context.Products.FindAsync(request.ProductId);
                if (product == null)
                {
                    return false;
                }

                var productCategory = new ProductCategory
                {
                    CategoryId = (byte)request.CategoryId,
                    ProductId = request.ProductId
                };

                _context.ProductCategories.Add(productCategory);
                await _context.SaveChangesAsync();

                return true;
            }
        }
    }
}
