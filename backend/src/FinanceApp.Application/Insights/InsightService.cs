using FinanceApp.Application.DTOs;

namespace FinanceApp.Application.Insights;

public class InsightService(IEnumerable<IInsightRule> rules)
{
    public IEnumerable<InsightDto> Generate(InsightContext context) =>
        rules.SelectMany(r => r.Evaluate(context));
}
