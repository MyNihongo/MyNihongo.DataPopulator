﻿using MyNihongo.DataPopulator.Models.Kanji;

namespace MyNihongo.DataPopulator.Services;

internal interface IKanjiProvider
{
	IAsyncEnumerable<KanjiJsonRecord> GetKanjiAsync(CancellationToken ct = default);

	Task<IReadOnlyDictionary<char, KanjiReadingJsonRecord>> GetArchaicReadingsAsync(CancellationToken ct = default);
}