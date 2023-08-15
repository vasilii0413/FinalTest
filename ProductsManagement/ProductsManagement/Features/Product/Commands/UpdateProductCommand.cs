using MediatR;
using ProductsManagement.AppDbContext;

namespace ProductsManagement.Features.Product.Commands
{
    public record UpdateProductCommand(ProductModel Product) : IRequest<ProductModel>;
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ProductModel>
    {
        private readonly AppDBContext _context;
        public UpdateProductHandler(AppDBContext context)
        {
            _context = context;
        }
        public async Task<ProductModel> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            ProductModel updatedProduct = request.Product;
            _context.Products.Update(updatedProduct);
            await _context.SaveChangesAsync();
            return updatedProduct;
        }
    }
}