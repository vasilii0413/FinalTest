using MediatR;
using ProductsManagement.AppDbContext;

namespace ProductsManagement.Features.Product.Commands
{
    public record DeleteProductCommand(ProductModel Product) : IRequest<ProductModel>;
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ProductModel>
    {
        private readonly AppDBContext _context;
        public DeleteProductHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<ProductModel> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            ProductModel deletedProduct = request.Product;
            _context.Products.Remove(deletedProduct);
            await _context.SaveChangesAsync();
            return deletedProduct;
        }
    }
}