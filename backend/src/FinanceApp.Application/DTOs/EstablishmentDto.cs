namespace FinanceApp.Application.DTOs;

public record EstablishmentDto(Guid Id, string Name);

public record CreateEstablishmentDto(string Name);

public record UpdateEstablishmentDto(string Name);
