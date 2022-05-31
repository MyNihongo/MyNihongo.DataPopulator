namespace MyNihongo.DataPopulator.Database.Models.Grammar;

[Table("tblGrammarRuleSearchByLanguage")]
public sealed record GrammarRuleSearchByLanguageDatabaseRecord
{
	[Column("langID", IsPrimaryKey = true, PrimaryKeyOrder = 1)]
	public Language Language { get; init; }

	[Column("text", IsPrimaryKey = true, PrimaryKeyOrder = 2, CanBeNull = false, Length = byte.MaxValue)]
	public string SearchText { get; init; } = string.Empty;

	[Column("grammarRuleId", IsPrimaryKey = true, PrimaryKeyOrder = 3)]
	public int GrammarRuleId { get; init; }
}
