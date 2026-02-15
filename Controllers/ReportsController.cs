using FamilyBudgetExpenseTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudgetExpenseTracker.Controllers
{
    // Optional API controller (Blazor pages do not require it). Safe for future API use.
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ReportsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("monthly-total")]
        public async Task<ActionResult<decimal>> GetMonthlyTotal([FromQuery] int userId, [FromQuery] int year, [FromQuery] int month)
        {
            if (userId <= 0) return Unauthorized();
            if (month < 1 || month > 12) return BadRequest("Month must be 1-12.");

            var total = await _db.Expenses
                .AsNoTracking()
                .Where(e => e.UserId == userId && e.Date.Year == year && e.Date.Month == month)
                .SumAsync(e => (double?)e.Amount) ?? 0d;

            return Ok((decimal)total);
        }
    }
}
