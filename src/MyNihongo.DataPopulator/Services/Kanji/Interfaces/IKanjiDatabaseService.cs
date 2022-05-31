namespace MyNihongo.DataPopulator.Services.Kanji;

public interface IKanjiDatabaseService
{
	Task<IReadOnlyDictionary<short, int>> GetHashCodesAsync(CancellationToken ct = default);
}