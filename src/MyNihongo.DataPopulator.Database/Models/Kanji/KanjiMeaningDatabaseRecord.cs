namespace MyNihongo.DataPopulator.Database.Models.Kanji;

[Table("tblKanjiMeaning")]
public sealed record KanjiMeaningDatabaseRecord
{
	[Column("kanjiID", IsPrimaryKey = true, PrimaryKeyOrder = 0)]
	public short KanjiId { get; init; }

	[Column("langID", IsPrimaryKey = true, PrimaryKeyOrder = 1)]
	public Language Language { get; init; }

	[Column("text", CanBeNull = false, IsPrimaryKey = true, PrimaryKeyOrder = 2)]
	public string Text { get; init; } = string.Empty;

	[Column("sorting")]
	public byte SortingOrder { get; init; }
}