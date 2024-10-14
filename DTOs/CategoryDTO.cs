using CardShop.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CardShop.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }

        [Required]
        [DisplayName("Category Name")]
        public string? Name { get; set; }

        [Required]
        public string? ImageUrl { get; set; }
    }
}
