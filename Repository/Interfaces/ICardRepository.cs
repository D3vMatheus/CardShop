using CardShop.Model;

namespace CardShop.Repository.Interfaces
{
    public interface ICardRepository : IRepository<Card>
    {
        Task<IEnumerable<Card>> GetByCardNumberAsync(string cardNumber);
    }
}
