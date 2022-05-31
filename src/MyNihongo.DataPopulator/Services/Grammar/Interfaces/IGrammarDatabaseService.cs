namespace MyNihongo.DataPopulator.Services.Grammar;

public interface IGrammarDatabaseService
{
	Task<IReadOnlyDictionary<int, int>> GetRuleHashCodeAsync(CancellationToken ct = default);
}
