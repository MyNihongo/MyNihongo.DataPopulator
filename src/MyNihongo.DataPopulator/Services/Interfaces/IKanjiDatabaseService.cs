namespace MyNihongo.DataPopulator.Services;

public interface IKanjiDatabaseService
{
	Task<IReadOnlyDictionary<short, int>> GetHashCodesAsync(CancellationToken ct = default);
}