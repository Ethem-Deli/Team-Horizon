using FamilyBudgetExpenseTracker.Data;
using FamilyBudgetExpenseTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudgetExpenseTracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/expenses/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Expense>>> GetUserExpenses(int userId)
        {
            var expenses = await _context.Expenses
                .Include(e => e.Category)
                .Where(e => e.UserId == userId)
                .OrderByDescending(e => e.Date)
                .ToListAsync();

            return Ok(expenses);
        }

        // GET: api/expenses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id, [FromQuery] int userId)
        {
            var expense = await _context.Expenses
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (expense == null)
                return NotFound();

            return Ok(expense);
        }

        // POST: api/expenses
        [HttpPost]
        public async Task<ActionResult<Expense>> CreateExpense(Expense expense)
        {
            // UserId MUST be set before saving
            if (expense.UserId == 0)
                return BadRequest("UserId is required.");

            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpense), new { id = expense.Id, userId = expense.UserId }, expense);
        }

        // PUT: api/expenses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, Expense updatedExpense)
        {
            if (id != updatedExpense.Id)
                return BadRequest();

            var existingExpense = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == updatedExpense.UserId);

            if (existingExpense == null)
                return Forbid(); // 🚨 user trying to modify someone else's data

            existingExpense.Amount = updatedExpense.Amount;
            existingExpense.Description = updatedExpense.Description;
            existingExpense.Date = updatedExpense.Date;
            existingExpense.CategoryId = updatedExpense.CategoryId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/expenses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id, [FromQuery] int userId)
        {
            var expense = await _context.Expenses
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (expense == null)
                return Forbid();

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
