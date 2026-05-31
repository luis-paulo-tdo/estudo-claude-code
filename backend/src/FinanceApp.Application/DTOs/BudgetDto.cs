namespace FinanceApp.Application.DTOs;

public record BudgetDto(
    Guid Id,
    Guid CategoryId,
    string CategoryName,
    string? CategoryColor,
    int Year,
    int Month,
    decimal PlannedAmount);

public record CreateBudgetDto(Guid CategoryId, int Year, int Month, decimal PlannedAmount);

public record UpdateBudgetDto(decimal PlannedAmount);
