using CardShop.Context;
using CardShop.Model;
using CardShop.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CardShop.Repository.Services
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        
        public CardRepository(CardShopDbContext context) :base(context){
        }

        //public async Task<IEnumerable<Card>> GetAsync()
        //{
        //    return await _context.cards.Take(10).ToListAsync();
        //}

        public async Task<IEnumerable<Card>> GetByCardNumberAsync(string cardNumber)
        {
            var card = await _context.cards.Where(c => c.CardNumber == cardNumber).ToListAsync();

            if (!CardNumberExists(cardNumber))
                throw new ArgumentNullException(nameof(card));

            return card;
        }

        //public async Task<Card> GetByIdAsync(int id)
        //{
        //    var card = await _context.cards.FirstOrDefaultAsync(c => c.ProductId == id);

        //    if (card is null)
        //        throw new ArgumentNullException(nameof(card));

        //    return card;
        //}

        //public async Task<Card> CreateAsync(Card card)
        //{
        //    if (card is null)
        //        throw new ArgumentNullException(nameof(card));

        //    await _context.cards.AddAsync(card);
        //    await _context.SaveChangesAsync();

        //    return card;
        //}

        //public async Task<Card> UpdateAsync(Card card)
        //{
        //    if (card is null)
        //        throw new ArgumentNullException(nameof(card));
            
        //    _context.Entry(card).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return card;
        //}

        private bool CardNumberExists(string number)
        {
            return _context.cards.Any(n => n.CardNumber == number);
        }
    }
}
