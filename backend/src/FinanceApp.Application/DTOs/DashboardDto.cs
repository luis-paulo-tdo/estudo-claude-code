namespace FinanceApp.Application.DTOs;

public record DashboardDto(
    int Year,
    int Month,
    decimal TotalIncome,
    decimal TotalExpenses,
    decimal Balance,
    IEnumerable<CategorySummaryDto> CategorySummaries,
    IEnumerable<TransactionDto> RecentTransactions);

public record CategorySummaryDto(
    Guid CategoryId,
    string CategoryName,
    string? CategoryColor,
    decimal TotalSpent,
    decimal? PlannedAmount,
    BudgetStatus Status);

public enum BudgetStatus { Ok, Warning, Critical, NoBudget }
