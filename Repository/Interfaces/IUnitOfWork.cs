namespace CardShop.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        ICardRepository CardRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }

        Task CommitAsync();
    }
}
