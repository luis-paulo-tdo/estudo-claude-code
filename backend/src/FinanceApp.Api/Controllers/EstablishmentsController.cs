using FinanceApp.Application.DTOs;
using FinanceApp.Domain.Entities;
using FinanceApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstablishmentsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<EstablishmentDto>> GetAll() =>
        await db.Establishments
            .OrderBy(e => e.Name)
            .Select(e => new EstablishmentDto(e.Id, e.Name))
            .ToListAsync();

    [HttpPost]
    public async Task<ActionResult<EstablishmentDto>> Create(CreateEstablishmentDto dto)
    {
        var establishment = new Establishment { Id = Guid.NewGuid(), Name = dto.Name };
        db.Establishments.Add(establishment);
        await db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new EstablishmentDto(establishment.Id, establishment.Name));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateEstablishmentDto dto)
    {
        var establishment = await db.Establishments.FindAsync(id);
        if (establishment is null) return NotFound();
        establishment.Name = dto.Name;
        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var establishment = await db.Establishments.FindAsync(id);
        if (establishment is null) return NotFound();
        db.Establishments.Remove(establishment);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
