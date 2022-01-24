namespace MyNihongo.DataPopulator.SrcGen.Utils.Extensions;

internal static class PropertySymbolEx
{
	public static bool TryGetAttributeValue<T>(this IPropertySymbol @this, string attributeName, string parameterName, out T value)
	{
		value = default!;
		var attrs = @this.GetAttributes();

		for (var i = 0; i < attrs.Length; i++)
		{
			if (attrs[i].GetName() != attributeName)
				continue;

			for (var j = 0; j < attrs[i].NamedArguments.Length; j++)
			{
				if (attrs[i].NamedArguments[j].Key != parameterName)
					continue;

				if (attrs[i].NamedArguments[j].Value.Value is not T tValue)
					return false;

				value = tValue;
				return true;
			}
		}

		return false;
	}
}