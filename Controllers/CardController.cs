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
using CardShop.Repository.Interfaces;

namespace CardShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CardController> _logger;

        public CardController(IUnitOfWork unitOfWork,
                              ILogger<CardController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetcardsAsync()
        {
            var cards = await _unitOfWork.CardRepository.GetAllAsync();

            if(cards is null){
                _logger.LogWarning("Cards doesn't exist");
                return NotFound("Card not found");
            }
            return Ok(cards);
        }

        // GET: api/Cards/5
        [HttpGet("{id}", Name = "GetCardById")]
        public async Task<ActionResult<Card>> GetCardByIdAsync(int id)
        {
            var card = await _unitOfWork.CardRepository.GetAsync(p => p.ProductId == id);

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
            var card = await _unitOfWork.CardRepository.GetByCardNumberAsync(number);

            if (card is null)
            {
                _logger.LogWarning($"Card number {number} doesn't exist");
                return NotFound($"Card number {number} not found, need meet the following conditions XX0-000 or YY11-111");
            }

            return Ok(card);
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCardAsync(int id, Card card)
        {
            if (id != card.ProductId)
            {
                _logger.LogWarning($"Couldn't update card due invalid information detected");
                return BadRequest("Invalid information detected");
            }
            
            await _unitOfWork.CardRepository.UpdateAsync(card);
            await _unitOfWork.CommitAsync();

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

            var newCard = await _unitOfWork.CardRepository.CreateAsync(card);
            await _unitOfWork.CommitAsync();

            return new CreatedAtRouteResult("GetCardById", new { id = newCard.CardId }, newCard); 
        }
    }
}
