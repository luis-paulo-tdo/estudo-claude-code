using FinanceApp.Application.DTOs;
using FinanceApp.Domain.Enums;
using FinanceApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<DashboardDto> Get([FromQuery] int? month, [FromQuery] int? year)
    {
        var now = DateTime.Today;
        var m = month ?? now.Month;
        var y = year ?? now.Year;

        var transactions = await db.Transactions
            .Include(t => t.Category)
            .Include(t => t.Establishment)
            .Where(t => t.Date.Month == m && t.Date.Year == y)
            .ToListAsync();

        var budgets = await db.Budgets
            .Include(b => b.Category)
            .Where(b => b.Month == m && b.Year == y)
            .ToListAsync();

        var totalIncome = transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount);
        var totalExpenses = transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);

        var expensesByCategory = transactions
            .Where(t => t.Type == TransactionType.Expense)
            .GroupBy(t => t.CategoryId)
            .ToDictionary(g => g.Key, g => (Amount: g.Sum(t => t.Amount), Category: g.First().Category));

        var categorySummaries = expensesByCategory.Select(kvp =>
        {
            var budget = budgets.FirstOrDefault(b => b.CategoryId == kvp.Key);
            var status = budget is null ? BudgetStatus.NoBudget
                : kvp.Value.Amount >= budget.PlannedAmount ? BudgetStatus.Critical
                : kvp.Value.Amount >= budget.PlannedAmount * 0.8m ? BudgetStatus.Warning
                : BudgetStatus.Ok;

            return new CategorySummaryDto(
                kvp.Key,
                kvp.Value.Category.Name,
                kvp.Value.Category.Color,
                kvp.Value.Amount,
                budget?.PlannedAmount,
                status);
        }).OrderByDescending(s => s.TotalSpent);

        var recentTransactions = transactions
            .OrderByDescending(t => t.Date)
            .Take(10)
            .Select(t => new TransactionDto(
                t.Id, t.Date, t.Amount, t.Description, t.Type,
                t.CategoryId, t.Category.Name, t.Category.Color,
                t.IsRecurring, t.RecurrenceDay,
                t.EstablishmentId, t.Establishment?.Name,
                t.UnitPrice, t.Quantity, t.Unit));

        return new DashboardDto(y, m, totalIncome, totalExpenses, totalIncome - totalExpenses, categorySummaries, recentTransactions);
    }
}
