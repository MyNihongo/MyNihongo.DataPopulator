using CommandLine;

namespace MyNihongo.DataPopulator;

public sealed record Args
{
	[Option('c')]
	public string ConnectionString { get; init; } = string.Empty;
}