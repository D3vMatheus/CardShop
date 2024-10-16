using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CardShop.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$", ErrorMessage = "Price value must have 2 digits after '.'")]
        public decimal Price { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

    }
}
