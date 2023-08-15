using MediatR;
using ProductsManagement.AppDbContext;

namespace ProductsManagement.Features.Product.Commands
{
    public record AddProductCommand(ProductModel Product) : IRequest<ProductModel>;
    public class AddProductHandler : IRequestHandler<AddProductCommand, ProductModel>
    {
        private readonly AppDBContext _context;
        public AddProductHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<ProductModel> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            ProductModel newProduct = request.Product;
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return newProduct;
        }
    }
}