namespace MyNihongo.DataPopulator.Services.Grammar;

public interface IGrammarPopulator
{
	public ValueTask PopulateRulesAsync(CancellationToken ct = default);
}
