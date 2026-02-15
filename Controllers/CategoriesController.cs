using FamilyBudgetExpenseTracker.Data;
using FamilyBudgetExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudgetExpenseTracker.Controllers
{
    // Optional API controller (Blazor pages do not require it). Safe for future API use.
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CategoriesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("user/{userId:int}")]
        public async Task<ActionResult<List<Category>>> GetUserCategories(int userId)
        {
            if (userId <= 0) return Unauthorized();

            var items = await _db.Categories
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .OrderBy(c => c.Name)
                .ToListAsync();

            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Create([FromBody] Category category)
        {
            if (category == null || category.UserId <= 0) return BadRequest();

            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return Ok(category);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Category>> Update(int id, [FromBody] Category category)
        {
            if (category == null || category.UserId <= 0) return BadRequest();

            var existing = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == category.UserId);
            if (existing == null) return NotFound();

            existing.Name = category.Name;
            existing.Icon = category.Icon;
            existing.ColorCode = category.ColorCode;

            _db.Categories.Update(existing);
            await _db.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id, [FromQuery] int userId)
        {
            if (userId <= 0) return Unauthorized();

            var existing = await _db.Categories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
            if (existing == null) return NotFound();

            _db.Categories.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
