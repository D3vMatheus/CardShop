using CardShop.Model;

namespace CardShop.Repository.Interfaces
{
    public interface ICardsRepository
    {
        Task<IEnumerable<Card>> GetCardAsync();
        Task<Card> GetCardByIdAsync(int id);
        Task<Card> GetCardByCardNumberAsync(string cardNumber);
        Task<Card> CreateCardAsync(Card card);
        Task<Card> UpdateCardAsync(Card card);
    }
}
