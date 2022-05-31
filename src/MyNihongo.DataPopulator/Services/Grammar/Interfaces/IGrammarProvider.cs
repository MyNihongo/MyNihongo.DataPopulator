using MyNihongo.DataPopulator.Models.Grammar;

namespace MyNihongo.DataPopulator.Services.Grammar;

public interface IGrammarProvider
{
	IAsyncEnumerable<GrammarRuleJsonRecord> GetRulesAsync(CancellationToken ct = default);

	Task<GrammarRuleContentRecord> GetRuleContentAsync(string ruleFileName, CancellationToken ct = default);
}
