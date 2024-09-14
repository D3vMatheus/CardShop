using CardShop.Model;
using Microsoft.EntityFrameworkCore;

namespace CardShop.Context
{
    //Db Context function: mapping domain entities to database
    //Operation management
    //sql queries (insert/delete/update...)
    public class CardShopDbContext : DbContext
    {
        public CardShopDbContext(DbContextOptions<CardShopDbContext> options) : base(options)
        {}

        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Card> cards { get; set; }
    }
}
