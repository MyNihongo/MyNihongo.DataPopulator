namespace MyNihongo.DataPopulator.SrcGen.Utils.Extensions;

internal static class StringBuilderEx
{
	public static StringBuilder AppendUsings(this StringBuilder @this, params string[] usings)
	{
		for (var i = 0; i < usings.Length; i++)
		{
			@this
				.AppendFormat("using {0};", usings[i])
				.AppendLine();
		}

		return @this;
	}

	public static StringBuilder AppendNamespace(this StringBuilder @this, Compilation compilation)
	{
		var @namespace = string.IsNullOrEmpty(compilation.AssemblyName)
			? "MyNihongo"
			: compilation.AssemblyName;

		return @this
			.AppendFormat("namespace {0};", @namespace)
			.AppendLine();
	}
}