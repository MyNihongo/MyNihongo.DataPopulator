namespace MyNihongo.DataPopulator.Models.Grammar;

[JsonSerializable(typeof(GrammarRuleJsonRecord[]))]
internal partial class GrammarRuleJsonContext : JsonSerializerContext
{
}

public sealed record GrammarRuleJsonRecord
{
	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("sort")]
	public short SortingOrder { get; set; }

	[JsonPropertyName("title")]
	public string Title { get; set; } = string.Empty;

	[JsonPropertyName("form")]
	public string Form { get; set; } = string.Empty;

	[JsonPropertyName("content")]
	public string Content { get; set; } = string.Empty;

	[JsonPropertyName("jlpt")]
	public JlptLevel JlptLevel { get; set; }

	[JsonPropertyName("jp")]
	public string[] SearchJapanese { get; set; } = Array.Empty<string>();

	[JsonPropertyName("en")]
	public string[] SearchEnglish { get; set; } = Array.Empty<string>();
}
