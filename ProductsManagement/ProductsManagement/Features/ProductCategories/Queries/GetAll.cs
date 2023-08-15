using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductsManagement.AppDbContext;
using System.Collections.Generic;

namespace ProductsManagement.Features.ProductCategories.Queries
{
    public class GetAllProductCategoryQuery : IRequest<List<ProductCategoryDto>> { }

    public class ProductCategoryDto
    {
        public byte CategoryId { get; set; }
        public string? CategoryType { get; set; }
        public short ProductId { get; set; }
        public string? ProductName { get; set; }
    }

    public class GetAllProductCategoryQueryHandler : IRequestHandler<GetAllProductCategoryQuery, List<ProductCategoryDto>>
    {
        private readonly AppDBContext _context;

        public GetAllProductCategoryQueryHandler(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<ProductCategoryDto>> Handle(GetAllProductCategoryQuery request, CancellationToken cancellationToken)
        {
            var productCategories = await _context.ProductCategories
                .Include(c => c.Product)
                .Include(c => c.Category)
                .ToListAsync();

            var dtoList = productCategories.Select(pc => new ProductCategoryDto
            {
                CategoryId = pc.CategoryId,
                CategoryType = pc.Category.CategoryType,
                ProductId = pc.ProductId,
                ProductName = pc.Product.ProductName
            }).ToList();

            return dtoList;
        }
    }
}
