using ProductsManagement.Features.Category;
using ProductsManagement.Features.Product;

namespace ProductsManagement.Features.ProductCategories
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public byte CategoryId { get; set; }
        public short ProductId { get; set; }
        public ProductModel? Product { get; set; }
        public CategoryModel? Category { get; set; }
    }

}
