using Microsoft.AspNetCore.Identity;
using ProductsManagement.Features.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductsManagement.Features.WishList
{
    [Table("WishList")]
    public class WishListModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short WishListId { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public ProductModel Product { get; set; }

    }
    public class WishListDto
    {
        public string Username { get; set; }
        public string ProductName { get; set; }
        public int WishlistId { get; set; }
    }
}
