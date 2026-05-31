using FinanceApp.Application.DTOs;
using FinanceApp.Domain.Entities;
using FinanceApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BudgetsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<BudgetDto>> GetAll([FromQuery] int? month, [FromQuery] int? year)
    {
        var query = db.Budgets.Include(b => b.Category).AsQueryable();

        if (month.HasValue) query = query.Where(b => b.Month == month.Value);
        if (year.HasValue) query = query.Where(b => b.Year == year.Value);

        return await query
            .OrderBy(b => b.Category.Name)
            .Select(b => new BudgetDto(b.Id, b.CategoryId, b.Category.Name, b.Category.Color, b.Year, b.Month, b.PlannedAmount))
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<BudgetDto>> Create(CreateBudgetDto dto)
    {
        var category = await db.Categories.FindAsync(dto.CategoryId);
        if (category is null) return BadRequest("Categoria não encontrada.");

        var exists = await db.Budgets.AnyAsync(b =>
            b.CategoryId == dto.CategoryId && b.Year == dto.Year && b.Month == dto.Month);
        if (exists) return Conflict("Já existe um orçamento para esta categoria neste mês.");

        var budget = new Budget
        {
            Id = Guid.NewGuid(),
            CategoryId = dto.CategoryId,
            Year = dto.Year,
            Month = dto.Month,
            PlannedAmount = dto.PlannedAmount,
            Category = category
        };

        db.Budgets.Add(budget);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new BudgetDto(budget.Id, budget.CategoryId, category.Name, category.Color, budget.Year, budget.Month, budget.PlannedAmount));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateBudgetDto dto)
    {
        var budget = await db.Budgets.FindAsync(id);
        if (budget is null) return NotFound();

        budget.PlannedAmount = dto.PlannedAmount;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var budget = await db.Budgets.FindAsync(id);
        if (budget is null) return NotFound();

        db.Budgets.Remove(budget);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
