namespace MyNihongo.DataPopulator.Database.Models.Grammar;

[Table("tblGrammarRuleContent")]
public sealed record GrammarRuleContentDatabaseRecord
{
	[Column("grammarRuleId", IsPrimaryKey = true, PrimaryKeyOrder = 1)]
	public int GrammarRuleId { get; init; }

	[Column("langID", IsPrimaryKey = true, PrimaryKeyOrder = 2)]
	public Language Language { get; init; }

	[Column("header", CanBeNull = false, Length = int.MaxValue)]
	public string Header { get; init; } = string.Empty;

	[Column("content", CanBeNull = false, Length = int.MaxValue)]
	public string Content { get; init; } = string.Empty;
}
