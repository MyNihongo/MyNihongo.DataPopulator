namespace MyNihongo.DataPopulator.Database.Models.Grammar;

[Table("tblGrammarRuleSearchByKanji")]
public sealed record GrammarRuleSearchByKanjiDatabaseRecord
{
	[Column("text", IsPrimaryKey = true, PrimaryKeyOrder = 1, CanBeNull = false, Length = byte.MaxValue, CreateFormat = $"{{0}} {{1}} COLLATE {Collations.KanjiKanaCollation} {{2}} {{3}}")]
	public string SearchText { get; init; } = string.Empty;

	[Column("grammarRuleId", IsPrimaryKey = true, PrimaryKeyOrder = 2)]
	public int GrammarRuleId { get; init; }
}
