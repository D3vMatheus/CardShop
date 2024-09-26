using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CardShop.Model
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage="Price value must have 2 digits after '.'")]
        public decimal Price { get; set; }
        
        [Required]
        public string? ImageUrl { get; set; }

        [Required]
        public float storage {get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int CategoryId { get; set; }
        
        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
