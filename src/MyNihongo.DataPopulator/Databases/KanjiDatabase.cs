using LinqToDB;
using MyNihongo.DataPopulator.Database.Models.Kanji;

namespace MyNihongo.DataPopulator.Databases;

internal sealed class KanjiDatabase : DatabaseBase
{
	public KanjiDatabase(string connectionString)
		: base(connectionString)
	{
	}

	public ITable<KanjiMasterDataDatabaseRecord> MasterData => GetTable<KanjiMasterDataDatabaseRecord>();

	public ITable<KanjiReadingDatabaseRecord> Readings => GetTable<KanjiReadingDatabaseRecord>();

	public ITable<KanjiMeaningDatabaseRecord> Meanings => GetTable<KanjiMeaningDatabaseRecord>();
}