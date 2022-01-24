using MyNihongo.DataPopulator.Models.Kanji;
using MyNihongo.DataPopulator.Utils.Extensions;

namespace MyNihongo.DataPopulator.Services;

internal sealed class KanjiProvider : IKanjiProvider
{
	private const string SourceDir = "Kanji";
	private readonly IFileSystemService _fileSystemService;

	public KanjiProvider(IFileSystemService fileSystemService)
	{
		_fileSystemService = fileSystemService;
	}

	public async IAsyncEnumerable<KanjiJsonRecord> GetKanjiAsync([EnumeratorCancellation] CancellationToken ct = default)
	{
		var resourceDir = _fileSystemService.GetResourceDirectory(SourceDir);

		var kanjiTask = Path.Combine(resourceDir, "data.json")
			.DeserialiseFileAsync(KanjiJsonContext.Default.KanjiJsonRecordArray, ct);

		var mappingTask = Path.Combine(resourceDir, "mapping.json")
			.DeserialiseFileAsync<Dictionary<char, short>>(ct: ct);

		await Task.WhenAll(kanjiTask, mappingTask)
			.ConfigureAwait(false);

		foreach (var kanji in kanjiTask.Result)
		{
			if (!mappingTask.Result.TryGetValue(kanji.Character, out var kanjiId))
				continue;

			kanji.KanjiId = kanjiId;
			yield return kanji;
		}
	}

	public async Task<IReadOnlyDictionary<char, KanjiReadingJsonRecord>> GetArchaicReadingsAsync(CancellationToken ct = default)
	{
		var resourceDir = _fileSystemService.GetResourceDirectory(SourceDir);
		
		return await Path.Combine(resourceDir, "data_archaic_readings.json")
			.DeserialiseFileAsync(KanjiReadingJsonContext.Default.DictionaryCharKanjiReadingJsonRecord, ct)
			.ConfigureAwait(false);
	}
}