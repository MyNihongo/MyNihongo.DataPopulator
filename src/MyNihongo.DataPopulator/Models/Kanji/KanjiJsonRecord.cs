namespace MyNihongo.DataPopulator.Models.Kanji;

[JsonSerializable(typeof(KanjiJsonRecord[]))]
internal partial class KanjiJsonContext : JsonSerializerContext
{
}

public sealed record KanjiJsonRecord
{
	[JsonIgnore]
	public short KanjiId { get; set; }

	[JsonPropertyName("c")]
	public char Character { get; set; }

	[JsonPropertyName("sort")]
	public short SortingOrder { get; set; }

	[JsonPropertyName("jlpt")]
	public JlptLevel? JlptLevel { get; set; }

	[JsonPropertyName("kunYomi")]
	public string[] KunReadings { get; set; } = Array.Empty<string>();

	[JsonPropertyName("onYomi")]
	public string[] OnReadings { get; set; } = Array.Empty<string>();

	[JsonPropertyName("en")]
	public string[] EnglishMeanings { get; set; } = Array.Empty<string>();

	[JsonPropertyName("fr")]
	public string[] FrenchMeanings { get; set; } = Array.Empty<string>();

	[JsonPropertyName("es")]
	public string[] SpanishMeanings { get; set; } = Array.Empty<string>();

	[JsonPropertyName("pt")]
	public string[] PortugueseMeanings { get; set; } = Array.Empty<string>();
}