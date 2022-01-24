using LinqToDB;
using MyNihongo.DataPopulator.Databases;

namespace MyNihongo.DataPopulator.Services;

internal sealed class KanjiDatabaseService : IKanjiDatabaseService
{
	private readonly Args _args;

	public KanjiDatabaseService(Args args)
	{
		_args = args;
	}

	public async Task<IReadOnlyDictionary<short, int>> GetHashCodesAsync(CancellationToken ct = default)
	{
		await using var db = new KanjiDatabase(_args.ConnectionString);

		return await db.MasterData
			.Select(static x => new
			{
				x.KanjiId,
				x.HashCode
			})
			.ToDictionaryAsync(static x => x.KanjiId, static x => x.HashCode, ct)
			.ConfigureAwait(false);
	}
}