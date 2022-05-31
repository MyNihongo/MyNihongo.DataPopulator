using LinqToDB;
using MyNihongo.DataPopulator.Databases;

namespace MyNihongo.DataPopulator.Services.Kanji;

internal sealed class KanjiDatabaseService : IKanjiDatabaseService
{
	private readonly Args _args;

	public KanjiDatabaseService(Args args)
	{
		_args = args;
	}

	public async Task<IReadOnlyDictionary<short, int>> GetHashCodesAsync(CancellationToken ct = default)
	{
		await using var connection = new KanjiDatabase(_args.ConnectionString);

		return await connection.MasterData
			.Select(static x => new
			{
				x.KanjiId,
				x.HashCode
			})
			.ToDictionaryAsync(static x => x.KanjiId, static x => x.HashCode, ct)
			.ConfigureAwait(false);
	}
}
