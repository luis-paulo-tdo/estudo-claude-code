using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.DTOs;

public record BulkTransactionItemDto(
    Guid CategoryId,
    string Description,
    decimal Amount,
    decimal? Quantity,
    string? Unit,
    decimal? UnitPrice);

public record BulkCreateTransactionDto(
    DateTime Date,
    TransactionType Type,
    Guid? EstablishmentId,
    IEnumerable<BulkTransactionItemDto> Items);
