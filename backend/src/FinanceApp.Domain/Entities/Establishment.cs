namespace FinanceApp.Domain.Entities;

public class Establishment
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Transaction> Transactions { get; set; } = [];
}
