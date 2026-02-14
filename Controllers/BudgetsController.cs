using FamilyBudgetExpenseTracker.Data;
using FamilyBudgetExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudgetExpenseTracker.Controllers
{
    // Optional API controller (Blazor pages do not require it). Safe for future API use.
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public BudgetsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("user/{userId:int}")]
        public async Task<ActionResult<List<Budget>>> GetUserBudgets(int userId)
        {
            if (userId <= 0) return Unauthorized();

            var items = await _db.Budgets
                .AsNoTracking()
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.Year)
                .ThenByDescending(b => b.Month)
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("user/{userId:int}/{year:int}/{month:int}")]
        public async Task<ActionResult<Budget?>> GetBudgetForMonth(int userId, int year, int month)
        {
            if (userId <= 0) return Unauthorized();
            if (month < 1 || month > 12) return BadRequest("Month must be 1-12.");

            var budget = await _db.Budgets
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.UserId == userId && b.Year == year && b.Month == month);

            return Ok(budget);
        }

        [HttpPost]
        public async Task<ActionResult> Upsert([FromBody] Budget budget)
        {
            if (budget == null || budget.UserId <= 0) return BadRequest();
            if (budget.Month < 1 || budget.Month > 12) return BadRequest("Month must be 1-12.");

            var existing = await _db.Budgets
                .FirstOrDefaultAsync(b => b.UserId == budget.UserId && b.Year == budget.Year && b.Month == budget.Month);

            if (existing != null)
            {
                existing.Amount = budget.Amount;
                _db.Budgets.Update(existing);
            }
            else
            {
                _db.Budgets.Add(budget);
            }

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id, [FromQuery] int userId)
        {
            if (userId <= 0) return Unauthorized();

            var budget = await _db.Budgets.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
            if (budget == null) return NotFound();

            _db.Budgets.Remove(budget);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
