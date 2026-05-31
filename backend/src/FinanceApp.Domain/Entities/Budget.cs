namespace FinanceApp.Domain.Entities;

public class Budget
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal PlannedAmount { get; set; }

    public Category Category { get; set; } = null!;
}
