﻿using CardShop.Context;
using CardShop.Model;
using CardShop.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CardShop.Repository.Services
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(CardShopDbContext context) :base(context){
        }

        //public async Task<IEnumerable<Category>> GetAsync()
        //{
        //    return await _context.categories.Take(10).ToListAsync();

        //}

        //public async Task<Category> GetByIdAsync(int id)
        //{
        //    var category = await _context.categories.FirstOrDefaultAsync(c => c.CategoryId == id);

        //    if (category is null)
        //        throw new ArgumentNullException(nameof(category));

        //    return category;
        //}

        public async Task<IEnumerable<Category>> GetProductsInCategoryAsync()
        {
            var category = await _context.categories.Include(p => p.Products).ToListAsync();

            if (category is null)
                throw new ArgumentNullException(nameof(category));

            return category;
        }

        //public async Task<Category> CreateAsync(Category category)
        //{
        //    if(category is null)
        //        throw new ArgumentNullException(nameof(category));

        //    await _context.categories.AddAsync(category);
        //    await _context.SaveChangesAsync();

        //    return category;
        //}

        //public async Task<Category> UpdateAsync(Category category)
        //{
        //    if (category is null)
        //        throw new ArgumentNullException(nameof(category));

        //    _context.categories.Entry(category).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return category;
        //}

        public async Task<Category> DeleteAsync(int id)
        {
            var category = await _context.categories.FindAsync(id);
         
            if (category is null)
                throw new ArgumentNullException(nameof(category));
            
            _context.categories.Remove(category);
            await _context.SaveChangesAsync();
            
            return category;
        }
    }
}
