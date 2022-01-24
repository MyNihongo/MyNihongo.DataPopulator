namespace MyNihongo.DataPopulator.SrcGen.Models;

internal sealed record SourceGeneratorResultRecord(string HintName, string Source)
{
	public string HintName { get; } = HintName;

	public string Source { get; } = Source;

	public void Deconstruct(out string hintName, out string source)
	{
		hintName = HintName;
		source = Source;
	}
}