using FinanceApp.Application.DTOs;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.Insights.Rules;

public class NoEmergencyFundRule : IInsightRule
{
    public IEnumerable<InsightDto> Evaluate(InsightContext context)
    {
        var emergencyCategory = context.Categories
            .FirstOrDefault(c => c.Name.Contains("Reserva", StringComparison.OrdinalIgnoreCase));

        if (emergencyCategory is null) yield break;

        var hasEmergencyTransactions = context.CurrentTransactions
            .Any(t => t.CategoryId == emergencyCategory.Id && t.Type == TransactionType.Income);

        if (hasEmergencyTransactions) yield break;

        var avgMonthlyExpenses = context.PreviousTransactions
            .Where(t => t.Type == TransactionType.Expense)
            .Sum(t => t.Amount);

        var targetReserve = avgMonthlyExpenses * 3;
        var message = targetReserve > 0
            ? $"Você não aportou na reserva de emergência este mês. Com base nos seus gastos, o ideal é ter R$ {targetReserve:N0} guardados (3 meses de despesas)."
            : "Você não aportou na reserva de emergência este mês. Esse é o primeiro passo da saúde financeira.";

        yield return new InsightDto(
            "NoEmergencyFund",
            InsightSeverity.Warning,
            message,
            emergencyCategory.Id);
    }
}
