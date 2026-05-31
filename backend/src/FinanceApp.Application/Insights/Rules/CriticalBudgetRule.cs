using FinanceApp.Application.DTOs;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.Insights.Rules;

public class CriticalBudgetRule : IInsightRule
{
    public IEnumerable<InsightDto> Evaluate(InsightContext context)
    {
        var spentByCategory = context.CurrentTransactions
            .Where(t => t.Type == TransactionType.Expense)
            .GroupBy(t => t.CategoryId)
            .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));

        foreach (var budget in context.Budgets)
        {
            if (budget.PlannedAmount <= 0) continue;
            if (!spentByCategory.TryGetValue(budget.CategoryId, out var spent)) continue;

            var ratio = spent / budget.PlannedAmount;
            if (ratio <= 0.80m) continue;

            var lastTransactionDay = context.CurrentTransactions
                .Where(t => t.CategoryId == budget.CategoryId && t.Type == TransactionType.Expense)
                .Max(t => t.Date.Day);

            if (lastTransactionDay >= 20) continue;

            var category = context.Categories.FirstOrDefault(c => c.Id == budget.CategoryId);
            var categoryName = category?.Name ?? "categoria";
            var pct = (int)(ratio * 100);

            yield return new InsightDto(
                "CriticalBudget",
                InsightSeverity.Critical,
                $"Você usou {pct}% do orçamento de {categoryName} antes do dia 20.",
                budget.CategoryId);
        }
    }
}
