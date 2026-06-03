using FinanceApp.Domain.Enums;

namespace FinanceApp.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public TransactionType Type { get; set; }
    public Guid CategoryId { get; set; }
    public bool IsRecurring { get; set; }
    public int? RecurrenceDay { get; set; }

    public string? Establishment { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? Quantity { get; set; }
    public string? Unit { get; set; }

    public Category Category { get; set; } = null!;
}
