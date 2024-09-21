using CardShop.Enums;
using System.ComponentModel.DataAnnotations;

namespace CardShop.Model
{
    public class Card : Product
    {
        public int CardId { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{2}\d{1,2}-\d{3}$"]
        public string? CardNumber { get; set; }
        
        [Required]
        public Extra extra { get; set; }
        
        [Required]
        public Language Language { get; set; }
        
        [Required]
        public Quality Quality { get; set; }

        [Required]
        public Rarity Rarity { get; set; }

    }
}
