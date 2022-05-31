namespace MyNihongo.DataPopulator.Database.Models.Grammar;

[Table("tblGrammarRuleMasterData")]
public sealed record GrammarRuleMasterDataDatabaseRecord
{
	[Column("grammarRuleId", IsPrimaryKey = true)]
	public int GrammarRuleId { get; init; }

	[Column("sorting")]
	public short SortingOrder { get; init; }

	[Column("form", CanBeNull = false)]
	public string Form { get; init; } = string.Empty;

	[Column("jlptLevel")]
	public JlptLevel JlptLevel { get; init; }

	[Column("hash")]
	public int HashCode { get; init; }

	[Column("timestamp")]
	public long Timestamp { get; init; }
}
