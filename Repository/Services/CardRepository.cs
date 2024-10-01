using CardShop.Context;
using CardShop.Model;
using CardShop.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardShop.Repository.Services
{
    public class CardRepository : ICardRepository
    {
        private readonly CardShopDbContext _context;
        
        public CardRepository(CardShopDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<Card>> GetCardAsync()
        {
            return await _context.cards.Take(10).ToListAsync();
        }

        public async Task<Card> GetCardByCardNumberAsync(string cardNumber)
        {
            var card = await _context.cards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);

            if (card is null)
                throw new ArgumentNullException(nameof(card));

            return card;
        }

        public async Task<Card> GetCardByIdAsync(int id)
        {
            var card = await _context.cards.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (card is null)
                throw new ArgumentNullException(nameof(card));

            return card;
        }

        public async Task<Card> CreateCardAsync(Card card)
        {
            if (card is null)
                throw new ArgumentNullException(nameof(card));

            await _context.cards.AddAsync(card);
            await _context.SaveChangesAsync();

            return card;
        }

        public async Task<Card> UpdateCardAsync(Card card)
        {
            if (card is null)
                throw new ArgumentNullException(nameof(card));
            
            _context.Entry(card).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return card;
        }
    }
}
