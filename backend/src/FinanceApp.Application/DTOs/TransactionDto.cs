using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.DTOs;

public record TransactionDto(
    Guid Id,
    DateOnly Date,
    decimal Amount,
    string Description,
    TransactionType Type,
    Guid CategoryId,
    string CategoryName,
    string? CategoryColor,
    bool IsRecurring,
    int? RecurrenceDay);

public record CreateTransactionDto(
    DateOnly Date,
    decimal Amount,
    string Description,
    TransactionType Type,
    Guid CategoryId,
    bool IsRecurring,
    int? RecurrenceDay);

public record UpdateTransactionDto(
    DateOnly Date,
    decimal Amount,
    string Description,
    TransactionType Type,
    Guid CategoryId,
    bool IsRecurring,
    int? RecurrenceDay);
