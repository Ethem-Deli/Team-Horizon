using Microsoft.EntityFrameworkCore;
using FamilyBudgetExpenseTracker.Data;
using FamilyBudgetExpenseTracker.Models;

namespace FamilyBudgetExpenseTracker.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _db;

        public CategoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Category>> GetUserCategoriesAsync(int userId)
        {
            return await _db.Categories
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<int> GetCategoryCountAsync(int userId)
        {
            return await _db.Categories
                .Where(c => c.UserId == userId)
                .CountAsync();
        }

        // NEW: Method to retrieve a single category for editing
        public async Task<Category?> GetCategoryByIdAsync(int id, int userId)
        {
            return await _db.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        }

        public async Task<bool> AddCategoryAsync(Category category)
        {
            _db.Categories.Add(category);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCategoryAsync(Category category, int userId)
        {
            var existing = await _db.Categories
                .FirstOrDefaultAsync(c => c.Id == category.Id && c.UserId == userId);

            if (existing == null) return false;

            existing.Name = category.Name;
            existing.Description = category.Description;
            existing.ColorCode = category.ColorCode;
            existing.Icon = category.Icon;

            _db.Categories.Update(existing);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId, int userId)
        {
            var category = await _db.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);

            if (category == null) return false;

            _db.Categories.Remove(category);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}