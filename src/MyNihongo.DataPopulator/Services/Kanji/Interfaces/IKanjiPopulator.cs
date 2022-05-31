namespace MyNihongo.DataPopulator.Services.Kanji;

public interface IKanjiPopulator
{
	ValueTask PopulateKanjiAsync(CancellationToken ct = default);
}