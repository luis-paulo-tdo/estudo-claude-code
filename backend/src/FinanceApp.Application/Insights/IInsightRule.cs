using FinanceApp.Application.DTOs;

namespace FinanceApp.Application.Insights;

public interface IInsightRule
{
    IEnumerable<InsightDto> Evaluate(InsightContext context);
}
