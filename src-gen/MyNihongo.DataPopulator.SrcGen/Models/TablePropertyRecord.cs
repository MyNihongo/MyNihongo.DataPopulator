namespace MyNihongo.DataPopulator.SrcGen.Models;

internal sealed record TablePropertyRecord(string Name, string FieldName, INamedTypeSymbol EntityType)
{
	public string Name { get; } = Name;

	public string FieldName { get; } = FieldName;

	public INamedTypeSymbol EntityType { get; } = EntityType;
}