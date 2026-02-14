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
            // SECURITY: If user is not logged in, return empty list (no data leakage).
            if (userId <= 0) return new List<Category>();

            // SECURITY: Always filter by UserId so users only see their own categories.
            return await _db.Categories
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<int> GetCategoryCountAsync(int userId)
        {
            // SECURITY: If user is not logged in, return 0.
            if (userId <= 0) return 0;

            // SECURITY: Always filter by UserId so counts are for current user only.
            return await _db.Categories
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .CountAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id, int userId)
        {
            // SECURITY: Only allow fetch for a valid logged-in user.
            if (userId <= 0 || id <= 0) return null;

            // SECURITY: Fetch by Id + UserId to prevent access to other users' categories.
            return await _db.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
        }

        public async Task<bool> AddCategoryAsync(Category category)
        {
            // SECURITY: Do not allow inserting categories without a valid user.
            if (category == null || category.UserId <= 0) return false;

            NormalizeCategoryInput(category);

            // Prevent duplicate names per user (case-insensitive)
            var normalized = category.Name.ToLowerInvariant();
            var exists = await _db.Categories
                .AsNoTracking()
                .AnyAsync(c => c.UserId == category.UserId && c.Name.ToLower() == normalized);

            if (exists) return false;

            _db.Categories.Add(category);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCategoryAsync(Category category, int userId)
        {
            // SECURITY: Only allow updates for a valid logged-in user.
            if (category == null || userId <= 0) return false;

            NormalizeCategoryInput(category);

            // SECURITY: Update only if the category belongs to this user (Id + UserId check).
            var existing = await _db.Categories
                .FirstOrDefaultAsync(c => c.Id == category.Id && c.UserId == userId);

            if (existing == null) return false;

            // Prevent duplicate names per user (case-insensitive), excluding current id
            var normalized = category.Name.ToLowerInvariant();
            var duplicate = await _db.Categories
                .AsNoTracking()
                .AnyAsync(c => c.UserId == userId && c.Id != category.Id && c.Name.ToLower() == normalized);

            if (duplicate) return false;

            // Update only allowed fields (ownership stays unchanged).
            existing.Name = category.Name;
            existing.Description = category.Description;
            existing.ColorCode = category.ColorCode;
            existing.Icon = category.Icon;

            _db.Categories.Update(existing);
            return await _db.SaveChangesAsync() > 0;
        }

        private static void NormalizeCategoryInput(Category category)
        {
            category.Name = (category.Name ?? string.Empty).Trim();
            category.Description = string.IsNullOrWhiteSpace(category.Description) ? null : category.Description.Trim();
            category.Icon = string.IsNullOrWhiteSpace(category.Icon) ? "📁" : category.Icon.Trim();
            category.ColorCode = string.IsNullOrWhiteSpace(category.ColorCode) ? "#3498db" : category.ColorCode.Trim();
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId, int userId)
        {
            // SECURITY: Only allow delete for a valid logged-in user.
            if (userId <= 0 || categoryId <= 0) return false;

            // SECURITY: Delete only if category belongs to this user (Id + UserId check).
            var category = await _db.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);

            if (category == null) return false;

            _db.Categories.Remove(category);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
