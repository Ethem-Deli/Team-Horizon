using FamilyBudgetExpenseTracker.Models;

namespace FamilyBudgetExpenseTracker.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetUserCategoriesAsync(int userId);
        Task<bool> AddCategoryAsync(Category category);
        Task<bool> UpdateCategoryAsync(Category category, int userId);
        Task<Category?> GetCategoryByIdAsync(int id, int userId);
        Task<bool> DeleteCategoryAsync(int categoryId, int userId);
        Task<int> GetCategoryCountAsync(int userId);
    }
}