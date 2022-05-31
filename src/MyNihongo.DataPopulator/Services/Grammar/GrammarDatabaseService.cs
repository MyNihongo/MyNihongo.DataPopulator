using LinqToDB;
using MyNihongo.DataPopulator.Databases;

namespace MyNihongo.DataPopulator.Services.Grammar;

internal sealed class GrammarDatabaseService : IGrammarDatabaseService
{
	private readonly Args _args;

	public GrammarDatabaseService(Args args)
	{
		_args = args;
	}

	public async Task<IReadOnlyDictionary<int, int>> GetRuleHashCodeAsync(CancellationToken ct = default)
	{
		await using var connection = new GrammarDatabase(_args.ConnectionString);

		return await connection.RuleMasterData
			.Select(static x => new
			{
				x.GrammarRuleId,
				x.HashCode
			})
			.ToDictionaryAsync(static x => x.GrammarRuleId, static x => x.HashCode, ct)
			.ConfigureAwait(false);
	}
}
