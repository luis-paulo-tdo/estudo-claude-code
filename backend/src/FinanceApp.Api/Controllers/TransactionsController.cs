using FinanceApp.Application.DTOs;
using FinanceApp.Domain.Entities;
using FinanceApp.Domain.Enums;
using FinanceApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<TransactionDto>> GetAll(
        [FromQuery] int? month,
        [FromQuery] int? year,
        [FromQuery] Guid? categoryId,
        [FromQuery] TransactionType? type)
    {
        var query = db.Transactions.Include(t => t.Category).AsQueryable();

        if (month.HasValue) query = query.Where(t => t.Date.Month == month.Value);
        if (year.HasValue) query = query.Where(t => t.Date.Year == year.Value);
        if (categoryId.HasValue) query = query.Where(t => t.CategoryId == categoryId.Value);
        if (type.HasValue) query = query.Where(t => t.Type == type.Value);

        return await query
            .OrderByDescending(t => t.Date)
            .Select(t => MapToDto(t))
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<TransactionDto>> Create(CreateTransactionDto dto)
    {
        var category = await db.Categories.FindAsync(dto.CategoryId);
        if (category is null) return BadRequest("Categoria não encontrada.");

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Date = dto.Date,
            Amount = dto.Amount,
            Description = dto.Description,
            Type = dto.Type,
            CategoryId = dto.CategoryId,
            IsRecurring = dto.IsRecurring,
            RecurrenceDay = dto.RecurrenceDay,
            Establishment = dto.Establishment,
            UnitPrice = dto.UnitPrice,
            Quantity = dto.Quantity,
            Unit = dto.Unit,
            Category = category
        };

        db.Transactions.Add(transaction);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), MapToDto(transaction));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateTransactionDto dto)
    {
        var transaction = await db.Transactions.FindAsync(id);
        if (transaction is null) return NotFound();

        var categoryExists = await db.Categories.AnyAsync(c => c.Id == dto.CategoryId);
        if (!categoryExists) return BadRequest("Categoria não encontrada.");

        transaction.Date = dto.Date;
        transaction.Amount = dto.Amount;
        transaction.Description = dto.Description;
        transaction.Type = dto.Type;
        transaction.CategoryId = dto.CategoryId;
        transaction.IsRecurring = dto.IsRecurring;
        transaction.RecurrenceDay = dto.RecurrenceDay;
        transaction.Establishment = dto.Establishment;
        transaction.UnitPrice = dto.UnitPrice;
        transaction.Quantity = dto.Quantity;
        transaction.Unit = dto.Unit;

        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var transaction = await db.Transactions.FindAsync(id);
        if (transaction is null) return NotFound();

        db.Transactions.Remove(transaction);
        await db.SaveChangesAsync();
        return NoContent();
    }

    private static TransactionDto MapToDto(Transaction t) =>
        new(t.Id, t.Date, t.Amount, t.Description, t.Type,
            t.CategoryId, t.Category.Name, t.Category.Color,
            t.IsRecurring, t.RecurrenceDay,
            t.Establishment, t.UnitPrice, t.Quantity, t.Unit);
}
