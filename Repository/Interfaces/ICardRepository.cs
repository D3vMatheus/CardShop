using CardShop.Model;

namespace CardShop.Repository.Interfaces
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> GetAsync();
        Task<IEnumerable<Card>> GetByCardNumberAsync(string cardNumber);
        Task<Card> GetByIdAsync(int id);
        Task<Card> CreateAsync(Card card);
        Task<Card> UpdateAsync(Card card);
    }
}
