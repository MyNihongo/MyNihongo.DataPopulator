namespace MyNihongo.DataPopulator.SrcGen.Utils.Extensions;

internal static class TypeSymbolEx
{
	public static IEnumerable<IPropertySymbol> GetProperties(this ITypeSymbol @this)
	{
		while (true)
		{
			var props = @this.GetMembers()
				.OfType<IPropertySymbol>()
				.Where(x => x.DeclaredAccessibility != Accessibility.Private);

			foreach (var prop in props)
				yield return prop;

			if (@this.BaseType == null)
				yield break;

			@this = @this.BaseType;
		}
	}
}