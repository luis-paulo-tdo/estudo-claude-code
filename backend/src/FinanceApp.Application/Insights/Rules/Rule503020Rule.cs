using FinanceApp.Application.DTOs;
using FinanceApp.Domain.Enums;

namespace FinanceApp.Application.Insights.Rules;

// Simplified 50-30-20: all expenses counted as "needs" since we don't classify per category here.
// The rule alerts when total expenses exceed 80% of income (leaves no room for savings).
public class Rule503020Rule : IInsightRule
{
    private static readonly string[] NeedsCategories = ["Moradia", "Alimentação", "Saúde", "Transporte"];
    private static readonly string[] WantsCategories = ["Lazer", "Vestuário", "Educação"];

    public IEnumerable<InsightDto> Evaluate(InsightContext context)
    {
        var totalIncome = context.CurrentTransactions
            .Where(t => t.Type == TransactionType.Income)
            .Sum(t => t.Amount);

        if (totalIncome <= 0) yield break;

        var expensesByCategory = context.CurrentTransactions
            .Where(t => t.Type == TransactionType.Expense)
            .GroupBy(t => t.CategoryId)
            .ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));

        decimal needs = 0, wants = 0;
        foreach (var (catId, amount) in expensesByCategory)
        {
            var name = context.Categories.FirstOrDefault(c => c.Id == catId)?.Name ?? "";
            if (NeedsCategories.Contains(name)) needs += amount;
            else if (WantsCategories.Contains(name)) wants += amount;
        }

        var needsPct = (int)(needs / totalIncome * 100);
        var wantsPct = (int)(wants / totalIncome * 100);

        if (needsPct > 50)
            yield return new InsightDto(
                "Rule5030_Needs",
                InsightSeverity.Warning,
                $"Seus gastos com necessidades representam {needsPct}% da renda — o ideal é até 50% (regra 50-30-20).",
                null);

        if (wantsPct > 30)
            yield return new InsightDto(
                "Rule5030_Wants",
                InsightSeverity.Info,
                $"Seus gastos com desejos representam {wantsPct}% da renda — o ideal é até 30% (regra 50-30-20).",
                null);
    }
}
