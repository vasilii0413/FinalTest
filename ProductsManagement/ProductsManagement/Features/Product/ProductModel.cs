using ProductsManagement.Features.Category;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductsManagement.Features.Product
{
    [Table("Product")]
    public class ProductModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "smallint")]
        public short ProductId { get; set; }

        [StringLength(50), Column(TypeName = "nvarchar(50)")]
        public string? ProductName { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Image { get; set; }

        [Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; }
        [JsonIgnore]
        public ICollection<CategoryModel>? Categories { get; set; }
    }
}