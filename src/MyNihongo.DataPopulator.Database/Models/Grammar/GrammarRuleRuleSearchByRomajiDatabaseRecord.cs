namespace MyNihongo.DataPopulator.Database.Models.Grammar;

[Table("tblGrammarRuleSearchByRomaji")]
public sealed record GrammarRuleRuleSearchByRomajiDatabaseRecord
{
	[Column("text", IsPrimaryKey = true, PrimaryKeyOrder = 1, CanBeNull = false, Length = byte.MaxValue)]
	[DataType(DataType.VarChar)]
	public string SearchText { get; init; } = string.Empty;

	[Column("grammarRuleId", IsPrimaryKey = true, PrimaryKeyOrder = 2)]
	public int GrammarRuleId { get; init; }
}
