using CardShop.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CardShop.DTOs
{
    public class ProductDTOUpdateResponse
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public float storage { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
    }
}
