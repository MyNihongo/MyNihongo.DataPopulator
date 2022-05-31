namespace MyNihongo.DataPopulator.Database.Models.Kanji;

[Table("tblKanjiReading")]
public sealed record KanjiReadingDatabaseRecord
{
	[Column("kanjiID", IsPrimaryKey = true, PrimaryKeyOrder = 0)]
	public short KanjiId { get; init; }

	[Column("main", CanBeNull = false, IsPrimaryKey = true, PrimaryKeyOrder = 1, CreateFormat = $"{{0}} {{1}} COLLATE {Collations.KanjiKanaCollation} {{2}} {{3}}")]
	public string MainText { get; init; } = string.Empty;

	[Column("secondary", CanBeNull = false, IsPrimaryKey = true, PrimaryKeyOrder = 2, CreateFormat = $"{{0}} {{1}} COLLATE {Collations.KanjiKanaCollation} {{2}} {{3}}")]
	public string SecondaryText { get; init; } = string.Empty;

	[Column("sorting")]
	public byte SortingOrder { get; init; }

	[Column("type")]
	public KanjiReadingType ReadingType { get; init; }

	[Column("romaji")]
	public string Romaji { get; init; } = string.Empty;
}