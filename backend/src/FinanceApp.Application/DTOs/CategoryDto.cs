namespace FinanceApp.Application.DTOs;

public record CategoryDto(Guid Id, string Name, string? Color, bool IsDefault);

public record CreateCategoryDto(string Name, string? Color);

public record UpdateCategoryDto(string Name, string? Color);
