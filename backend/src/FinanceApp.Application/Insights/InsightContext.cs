using FinanceApp.Domain.Entities;

namespace FinanceApp.Application.Insights;

public class InsightContext
{
    public int Year { get; init; }
    public int Month { get; init; }
    public List<Transaction> CurrentTransactions { get; init; } = [];
    public List<Transaction> PreviousTransactions { get; init; } = [];
    public List<Budget> Budgets { get; init; } = [];
    public List<Category> Categories { get; init; } = [];
}
