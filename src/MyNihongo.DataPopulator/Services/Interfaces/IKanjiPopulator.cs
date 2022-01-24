namespace MyNihongo.DataPopulator.Services;

public interface IKanjiPopulator
{
	ValueTask PopulateKanjiAsync(CancellationToken ct = default);
}