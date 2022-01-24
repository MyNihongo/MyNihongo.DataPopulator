namespace MyNihongo.DataPopulator.Models.Kanji;

[JsonSerializable(typeof(Dictionary<char, KanjiReadingJsonRecord>))]
internal partial class KanjiReadingJsonContext : JsonSerializerContext
{
}

public sealed record KanjiReadingJsonRecord
{
	[JsonPropertyName("kunYomi")]
	public string[] KunReadings { get; set; } = Array.Empty<string>();

	[JsonPropertyName("onYomi")]
	public string[] OnReadings { get; set; } = Array.Empty<string>();
}