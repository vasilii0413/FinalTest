using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductsManagement.Features.Category;
using ProductsManagement.Features.Product;
using ProductsManagement.Features.ProductCategories;
using ProductsManagement.Features.WishList;

namespace ProductsManagement.AppDbContext
{
    public class AppDBContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public AppDBContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<WishListModel> WishLists { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

    }
}

