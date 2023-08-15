using ProductsManagement.Features.Product;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProductsManagement.Features.Category
{
    [Table("Category")]
    public class CategoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "tinyint")]
        public byte CategoryId { get; set; }

        [StringLength(50), Column(TypeName = "nvarchar(50)")]
        public string? CategoryType { get; set; }

        [JsonIgnore]
        public ICollection<ProductModel>? Products { get; set; }



    }
}