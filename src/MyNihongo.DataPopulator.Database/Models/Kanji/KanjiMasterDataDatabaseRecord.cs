namespace MyNihongo.DataPopulator.Database.Models.Kanji;

[Table("tblKanjiMasterData")]
public sealed record KanjiMasterDataDatabaseRecord
{
	[Column("kanjiID", IsPrimaryKey = true)]
	public short KanjiId { get; init; }

	[Column("sorting")]
	public short SortingOrder { get; init; }

	[Column("char")]
	public char Character { get; init; }

	[Column("jlptLevel")]
	public JlptLevel? JlptLevel { get; init; }

	[Column("hash")]
	public int HashCode { get; init; }

	[Column("timestamp")]
	public long Timestamp { get; init; }
}