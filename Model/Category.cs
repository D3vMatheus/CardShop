using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CardShop.Model
{
    public class Category
    {
        public Category()
        {
            Products = new Collection<Product>();
        }
        
        public int CategoryId { get; set; }
        
        //[Required]
        [DisplayName("Category Name")]
        public string? Name { get; set; }
        
        //[Required]
        public string? ImageUrl { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
