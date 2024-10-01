using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CardShop.Context;
using CardShop.Model;
using System.ComponentModel.DataAnnotations;

namespace CardShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly CardShopDbContext _context;
        private readonly ILogger<CardController> _logger;

        public CardController(CardShopDbContext context, ILogger<CardController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetcardsAsync()
        {
            var cards = await _context.cards.AsNoTracking().Take(10).ToListAsync();
            if(cards is null){
                _logger.LogWarning("Cards doesn't exist");
                return NotFound("Card not found");
            }
            return cards;
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCardByIdAsync(int id)
        {
            var card = await _context.cards.FindAsync(id);

            if (card == null)
            {
                _logger.LogWarning("Card doesn't exist");
                return NotFound("Card not found");
            }

            return card;
        }

        [HttpGet("{number:regex(^[[A-Z]]{{2}}\\d{{1,2}}-\\d{{3}}$)}")]
        public async Task<ActionResult<IEnumerable<Card>>> GetCardByNumberAsync(string number)
        {
            var card = await _context.cards.Where(n => n.CardNumber == number).ToListAsync();

            if (!CardNumberExists(number))
            {
                _logger.LogWarning($"Card number {number} doesn't exist");
                return NotFound($"Card number {number} not found, need meet the following conditions XX0-000 or YY11-111");
            }

            return card;
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCardAsync(int id, Card card)
        {
            if (id != card.ProductId)
            {
                _logger.LogWarning($"Couldn't update card due invalid information detected");
                return BadRequest("Invalid information detected");
            }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
                {
                    _logger.LogWarning($"Couldn't update card due invalid information detected: {id} doesn't exist");
                    return NotFound("Card not found");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Card>> AddCardAsync(Card card)
        {
            if (card is null) 
            {
                _logger.LogWarning($"Couldn't add card due invalid information detected");
                return BadRequest("Invalid information detected");
            }

            _context.cards.Add(card);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCard", new { id = card.ProductId }, card);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardAsync(int id)
        {
            var card = await _context.cards.FindAsync(id);
            if (card == null)
            {
                _logger.LogWarning($"Couldn't delete card due invalid information detected: {id} doesn't exist");
                return NotFound("Card not found");
            }

            _context.cards.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(int id)
        {
            return _context.cards.Any(e => e.ProductId == id);
        }

        private bool CardNumberExists(string number)
        {
            return _context.cards.Any(n => n.CardNumber== number);
        }
    }
}
