using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.DTOs;

public record TransactionDto(
    Guid Id,
    DateTime Date,
    decimal Amount,
    string Description,
    TransactionType Type,
    Guid CategoryId,
    string CategoryName,
    string? CategoryColor,
    bool IsRecurring,
    int? RecurrenceDay,
    decimal? UnitPrice,
    decimal? Quantity,
    string? Unit);

public record CreateTransactionDto(
    DateTime Date,
    decimal Amount,
    string Description,
    TransactionType Type,
    Guid CategoryId,
    bool IsRecurring,
    int? RecurrenceDay,
    decimal? UnitPrice,
    decimal? Quantity,
    string? Unit);

public record UpdateTransactionDto(
    DateTime Date,
    decimal Amount,
    string Description,
    TransactionType Type,
    Guid CategoryId,
    bool IsRecurring,
    int? RecurrenceDay,
    decimal? UnitPrice,
    decimal? Quantity,
    string? Unit);
