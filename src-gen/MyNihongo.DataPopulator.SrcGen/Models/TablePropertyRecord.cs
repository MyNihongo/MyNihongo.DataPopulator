namespace MyNihongo.DataPopulator.SrcGen.Models;

internal sealed record TablePropertyRecord(string Name, string FieldName, INamedTypeSymbol EntityType)
{
	public string Name { get; } = Name;

	public string FieldName { get; } = FieldName;

	public INamedTypeSymbol EntityType { get; } = EntityType;

	public bool NeedsHashSet { get; } = Name.IndexOf("romaji", StringComparison.InvariantCultureIgnoreCase) != -1;

	public void Deconstruct(out string name, out string fieldName, out INamedTypeSymbol entityType)
	{
		name = Name;
		fieldName = FieldName;
		entityType = EntityType;
	}
}