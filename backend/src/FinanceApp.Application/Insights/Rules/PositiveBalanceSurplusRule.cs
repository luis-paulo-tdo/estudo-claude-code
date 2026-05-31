using FinanceApp.Application.DTOs;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.Insights.Rules;

public class PositiveBalanceSurplusRule : IInsightRule
{
    public IEnumerable<InsightDto> Evaluate(InsightContext context)
    {
        var totalIncome = context.CurrentTransactions
            .Where(t => t.Type == TransactionType.Income)
            .Sum(t => t.Amount);

        if (totalIncome <= 0) yield break;

        var totalExpenses = context.CurrentTransactions
            .Where(t => t.Type == TransactionType.Expense)
            .Sum(t => t.Amount);

        var surplus = totalIncome - totalExpenses;
        var surplusRatio = surplus / totalIncome;

        if (surplusRatio <= 0.30m) yield break;

        var pct = (int)(surplusRatio * 100);
        yield return new InsightDto(
            "PositiveBalanceSurplus",
            InsightSeverity.Info,
            $"Você teve uma sobra de {pct}% da renda este mês. Considere aportar esse valor em sua reserva de emergência ou investimentos.",
            null);
    }
}
