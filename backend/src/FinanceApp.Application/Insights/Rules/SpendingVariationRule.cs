using FinanceApp.Application.DTOs;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.Insights.Rules;

public class SpendingVariationRule : IInsightRule
{
    public IEnumerable<InsightDto> Evaluate(InsightContext context)
    {
        var currentByCategory = context.CurrentTransactions
            .Where(t => t.Type == TransactionType.Expense)
            .GroupBy(t => t.CategoryId)
            .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));

        var previousByCategory = context.PreviousTransactions
            .Where(t => t.Type == TransactionType.Expense)
            .GroupBy(t => t.CategoryId)
            .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));

        foreach (var (categoryId, currentAmount) in currentByCategory)
        {
            if (!previousByCategory.TryGetValue(categoryId, out var previousAmount) || previousAmount == 0)
                continue;

            var variation = (currentAmount - previousAmount) / previousAmount;
            if (variation <= 0.20m)
                continue;

            var category = context.Categories.FirstOrDefault(c => c.Id == categoryId);
            var categoryName = category?.Name ?? "categoria";
            var pct = (int)(variation * 100);

            yield return new InsightDto(
                "SpendingVariation",
                InsightSeverity.Warning,
                $"Seu gasto com {categoryName} cresceu {pct}% em relação ao mês anterior.",
                categoryId);
        }
    }
}
