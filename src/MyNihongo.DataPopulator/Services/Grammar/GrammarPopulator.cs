using MyNihongo.DataPopulator.Database.Models.Grammar;
using MyNihongo.DataPopulator.Models.Grammar;
using MyNihongo.DataPopulator.Utils.Helpers;

namespace MyNihongo.DataPopulator.Services.Grammar;

internal sealed class GrammarPopulator : IGrammarPopulator
{
	private readonly Args _args;
	private readonly IClock _clock;
	private readonly IGrammarProvider _grammarProvider;
	private readonly IGrammarDatabaseService _grammarDatabaseService;

	public GrammarPopulator(
		Args args,
		IClock clock,
		IGrammarProvider grammarProvider,
		IGrammarDatabaseService grammarDatabaseService)
	{
		_args = args;
		_clock = clock;
		_grammarProvider = grammarProvider;
		_grammarDatabaseService = grammarDatabaseService;
	}

	public async ValueTask PopulateRulesAsync(CancellationToken ct = default)
	{
		var populator = new GrammarDatabasePopulator(_args.ConnectionString);

		var hashCodeMapping = await _grammarDatabaseService.GetRuleHashCodeAsync(ct)
			.ConfigureAwait(false);

		await foreach (var grammarRule in _grammarProvider.GetRulesAsync(ct))
		{
			var hashCode = grammarRule.GetHashCodeEx(GrammarRuleJsonContext.Default.GrammarRuleJsonRecord);
			if (hashCodeMapping.TryGetValue(grammarRule.Id, out var currentHashCode) && currentHashCode == hashCode)
				continue;

			var ruleContent = await _grammarProvider.GetRuleContentAsync(grammarRule.Content, ct)
				.ConfigureAwait(false);

			populator.AddRuleMasterData(new GrammarRuleMasterDataDatabaseRecord
			{
				GrammarRuleId = grammarRule.Id,
				SortingOrder = grammarRule.SortingOrder,
				Form = grammarRule.Form,
				JlptLevel = grammarRule.JlptLevel,
				HashCode = hashCode,
				Timestamp = _clock.GetTicksNow()
			});

			populator.AddRuleContent(new GrammarRuleContentDatabaseRecord
			{
				GrammarRuleId = grammarRule.Id,
				Language = ruleContent.Language,
				Header = ruleContent.Header,
				Content = ruleContent.Content
			});

			PopulateSearchJapanese(populator, grammarRule);
			PopulateSearchLanguages(populator, grammarRule);
		}

		await populator.PopulateAsync(ct)
			.ConfigureAwait(false);
	}

	private static void PopulateSearchJapanese(GrammarDatabasePopulator populator, GrammarRuleJsonRecord grammarRule)
	{
		for (var i = 0; i < grammarRule.SearchJapanese.Length; i++)
		{
			if (!grammarRule.SearchJapanese[i].IsKanaOrKanji())
				throw new InvalidOperationException($"Japanese search must only be kana or kanji. Error in the grammar rule `{grammarRule.Id}`");

			if (grammarRule.SearchJapanese[i].HasKanji())
				populator.AddRuleSearchByKanji(new GrammarRuleSearchByKanjiDatabaseRecord
				{
					SearchText = grammarRule.SearchJapanese[i],
					GrammarRuleId = grammarRule.Id
				});
			else
				populator.AddRuleSearchByRomaji(new GrammarRuleRuleSearchByRomajiDatabaseRecord
				{
					SearchText = grammarRule.SearchJapanese[i].ToRomaji(),
					GrammarRuleId = grammarRule.Id
				});
		}
	}

	private static void PopulateSearchLanguages(GrammarDatabasePopulator populator, GrammarRuleJsonRecord grammarRule)
	{
		var hashSet = new SearchHashSet();

		for (var i = 0; i < grammarRule.SearchEnglish.Length; i++)
			hashSet.Add(grammarRule.SearchEnglish[i]);

		var englishSearch = hashSet
			.Select(x => new GrammarRuleSearchByLanguageDatabaseRecord
			{
				GrammarRuleId = grammarRule.Id,
				Language = Language.English,
				SearchText = x
			});

		populator.AddRuleSearchByLanguage(englishSearch);
	}
}
