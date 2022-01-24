namespace MyNihongo.DataPopulator.SrcGen.Utils.Extensions;

internal static class StringEx
{
	public static string CreateFieldName(this string @this)
	{
		if (string.IsNullOrEmpty(@this))
			return string.Empty;

		return "_" + char.ToLower(@this[0]) + @this.Substring(1);
	}
}