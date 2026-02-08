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

        public async Task<bool> AddCategoryAsync(Category category)
        {
            _db.Categories.Add(category);
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