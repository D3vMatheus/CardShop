using CardShop.Enums;

namespace CardShop.Model
{
    public class Card : Product
    {
        public int CardId { get; set; }
        public string? CardNumber { get; set; }
        public Extra extra { get; set; }
        public Language Language { get; set; }
        public Quality Quality { get; set; }

        public Rarity Rarity { get; set; }

    }
}
