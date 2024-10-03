using CardShop.Context;
using CardShop.Repository.Interfaces;

namespace CardShop.Repository.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly CardShopDbContext _context;

        private ICardRepository _cardRepo;

        private ICategoryRepository _categoryRepo;

        private IProductRepository _productRepo;

        public UnitOfWork(CardShopDbContext context)
        {
            _context = context;
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepo = ProductRepository ?? new ProductRepository(_context);
            }
        }
        public ICategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepo = CategoryRepository ?? new CategoryRepository(_context);
            }
        }
        public ICardRepository CardRepository
        {
            get
            {
                return _cardRepo= CardRepository ?? new CardRepository(_context);
            }
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
