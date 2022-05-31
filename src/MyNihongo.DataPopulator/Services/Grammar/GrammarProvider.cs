using MyNihongo.DataPopulator.Models.Grammar;

namespace MyNihongo.DataPopulator.Services.Grammar;

internal sealed class GrammarProvider : IGrammarProvider
{
	private const string SourceDir = "Grammar";
	private readonly IFileSystemService _fileSystemService;

	public GrammarProvider(IFileSystemService fileSystemService)
	{
		_fileSystemService = fileSystemService;
	}

	public async IAsyncEnumerable<GrammarRuleJsonRecord> GetRulesAsync([EnumeratorCancellation] CancellationToken ct = default)
	{
		var resourceDir = _fileSystemService.GetResourceDirectory(SourceDir);

		var rules = await Path.Combine(resourceDir, "rules.json")
			.DeserialiseFileAsync(GrammarRuleJsonContext.Default.GrammarRuleJsonRecordArray, ct)
			.ConfigureAwait(false);

		foreach (var rule in rules)
			yield return rule;
	}

	public async Task<GrammarRuleContentRecord> GetRuleContentAsync(string ruleFileName, CancellationToken ct = default)
	{
		var resourceDir = _fileSystemService.GetResourceDirectory(SourceDir);

		var fileName = $"{ruleFileName}.md";
		var headerFile = Path.Combine(resourceDir, "rules_header", fileName);
		var contentFile = Path.Combine(resourceDir, "rules_markdown", fileName);

		return new GrammarRuleContentRecord
		{
			Language = Language.English,
			Header = await _fileSystemService.GetContentAsync(headerFile).ConfigureAwait(false),
			Content = await _fileSystemService.GetContentAsync(contentFile).ConfigureAwait(false),
		};
	}
}
