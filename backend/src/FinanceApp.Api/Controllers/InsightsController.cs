using FinanceApp.Application.DTOs;
using FinanceApp.Application.Insights;
using FinanceApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InsightsController(AppDbContext db, InsightService insightService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<InsightDto>> Get([FromQuery] int? month, [FromQuery] int? year)
    {
        var now = DateTime.Today;
        var m = month ?? now.Month;
        var y = year ?? now.Year;

        var prevMonth = m == 1 ? 12 : m - 1;
        var prevYear = m == 1 ? y - 1 : y;

        var categories = await db.Categories.ToListAsync();

        var current = await db.Transactions
            .Where(t => t.Date.Month == m && t.Date.Year == y)
            .ToListAsync();

        var previous = await db.Transactions
            .Where(t => t.Date.Month == prevMonth && t.Date.Year == prevYear)
            .ToListAsync();

        var budgets = await db.Budgets
            .Where(b => b.Month == m && b.Year == y)
            .ToListAsync();

        var context = new InsightContext
        {
            Year = y,
            Month = m,
            CurrentTransactions = current,
            PreviousTransactions = previous,
            Budgets = budgets,
            Categories = categories
        };

        return insightService.Generate(context);
    }
}
