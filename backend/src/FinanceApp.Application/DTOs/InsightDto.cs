namespace FinanceApp.Application.DTOs;

public record InsightDto(
    string Type,
    InsightSeverity Severity,
    string Message,
    Guid? CategoryId);

public enum InsightSeverity { Info, Warning, Critical }
