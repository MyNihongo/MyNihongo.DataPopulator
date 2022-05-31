namespace MyNihongo.DataPopulator.Models.Grammar;

public sealed record GrammarRuleContentRecord
{
	public Language Language { get; init; }

	public string Header { get; init; } = string.Empty;

	public string Content { get; init; } = string.Empty;
}
