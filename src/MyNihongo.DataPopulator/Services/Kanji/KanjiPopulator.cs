using MyNihongo.DataPopulator.Database.Models.Kanji;
using MyNihongo.DataPopulator.Models.Kanji;

namespace MyNihongo.DataPopulator.Services.Kanji;

internal sealed class KanjiPopulator : IKanjiPopulator
{
	private static readonly ObjectPool<StringBuilder> StringBuilderPool = new DefaultObjectPoolProvider()
		.CreateStringBuilderPool();

	private readonly Args _args;
	private readonly IClock _clock;
	private readonly IKanjiProvider _kanjiProvider;
	private readonly IKanjiDatabaseService _kanjiDatabaseService;

	public KanjiPopulator(
		Args args,
		IClock clock,
		IKanjiProvider kanjiProvider,
		IKanjiDatabaseService kanjiDatabaseService)
	{
		_args = args;
		_clock = clock;
		_kanjiProvider = kanjiProvider;
		_kanjiDatabaseService = kanjiDatabaseService;
	}

	public async ValueTask PopulateKanjiAsync(CancellationToken ct = default)
	{
		var populator = new KanjiDatabasePopulator(_args.ConnectionString);

		var hashCodeMapping = await _kanjiDatabaseService.GetHashCodesAsync(ct)
			.ConfigureAwait(false);

		var archaicReadings = await _kanjiProvider.GetArchaicReadingsAsync(ct)
			.ConfigureAwait(false);

		await foreach (var kanji in _kanjiProvider.GetKanjiAsync(ct))
		{
			var hashCode = kanji.GetHashCodeEx(KanjiJsonContext.Default.KanjiJsonRecord);
			if (hashCodeMapping.TryGetValue(kanji.KanjiId, out var currentHashCode) && currentHashCode == hashCode)
				continue;

			populator.AddMasterData(new KanjiMasterDataDatabaseRecord
			{
				KanjiId = kanji.KanjiId,
				Character = kanji.Character,
				JlptLevel = kanji.JlptLevel,
				SortingOrder = kanji.SortingOrder,
				HashCode = hashCode,
				Timestamp = _clock.GetTicksNow()
			});

			archaicReadings.TryGetValue(kanji.Character, out var archaic);

			// Readings
			populator.AddReadings(CreateReadings(kanji.KanjiId, kanji.KunReadings, KanjiReadingType.Kun, archaic?.KunReadings));
			populator.AddReadings(CreateReadings(kanji.KanjiId, kanji.OnReadings, KanjiReadingType.On, archaic?.OnReadings));

			// Meanings
			populator.AddMeanings(CreateMeanings(kanji.KanjiId, kanji.EnglishMeanings, Language.English));
			populator.AddMeanings(CreateMeanings(kanji.KanjiId, kanji.FrenchMeanings, Language.French));
			populator.AddMeanings(CreateMeanings(kanji.KanjiId, kanji.SpanishMeanings, Language.Spanish));
			populator.AddMeanings(CreateMeanings(kanji.KanjiId, kanji.PortugueseMeanings, Language.Portuguese));
		}

		await populator.PopulateAsync(ct)
			.ConfigureAwait(false);
	}

	private static IEnumerable<KanjiReadingDatabaseRecord> CreateReadings(short kanjiId, IReadOnlyList<string> readings, KanjiReadingType type, string[]? archaic)
	{
		archaic ??= Array.Empty<string>();

		for (byte i = 0, j = 0; i < readings.Count; i++)
		{
			if (archaic.Contains(readings[i]))
				continue;

			var (main, secondary) = SplitText(readings[i], out var kanaReading);
			yield return new KanjiReadingDatabaseRecord
			{
				KanjiId = kanjiId,
				MainText = main,
				SecondaryText = secondary,
				SortingOrder = j++,
				ReadingType = type,
				Romaji = kanaReading.ToRomaji(StringBuilderPool.Get)
			};
		}

		static (string, string) SplitText(string reading, out string kanaReading)
		{
			string main = reading, secondary = string.Empty;
			var index = reading.IndexOf('|');

			if (index != -1)
			{
				main = reading[..index];
				secondary = reading[(index + 1)..];

				kanaReading = main + secondary;
			}
			else
			{
				kanaReading = reading;
			}

			return (main, secondary);
		}
	}

	private static IEnumerable<KanjiMeaningDatabaseRecord> CreateMeanings(short kanjiId, IReadOnlyList<string> meanings, Language language)
	{
		for (byte i = 0; i < meanings.Count; i++)
			yield return new KanjiMeaningDatabaseRecord
			{
				KanjiId = kanjiId,
				Text = meanings[i],
				SortingOrder = i,
				Language = language
			};
	}
}