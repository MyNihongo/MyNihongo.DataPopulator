using MyNihongo.DataPopulator.SrcGen.Models;
using MyNihongo.DataPopulator.SrcGen.Utils.Extensions;

namespace MyNihongo.DataPopulator.SrcGen.Utils;

internal static class SourceGenerator
{
	public static SourceGeneratorResultRecord GenerateSource(in this GeneratorExecutionContext @this, ClassDeclarationSyntax classSyntax)
	{
		const string connectionStringField = "_connectionString",
			databaseVar = "db", progressBarVar = "pb";

		var typeSymbol = classSyntax.GetTypeSymbol(@this.Compilation);
		var props = GetTables(typeSymbol.GetProperties());

		var className = $"{typeSymbol.Name}Populator";

		var stringBuilder = new StringBuilder()
			.AppendUsings("LinqToDB", "Konsole")
			.AppendNamespace(@this.Compilation)
			.AppendFormat("internal sealed class {0}", className).AppendLine()
			.AppendLine("{");

		// Create list fields
		foreach (var prop in props)
		{
			stringBuilder
				.AppendFormat("\tprivate readonly List<{0}> {1} = new();", prop.EntityType, prop.FieldName)
				.AppendLine();
		}

		// Ctor
		stringBuilder
			.AppendFormat("\tprivate readonly string {0};", connectionStringField).AppendLine()
			.AppendLine()
			.AppendFormat("\tpublic {0}(string connectionString)", className).AppendLine()
			.AppendLine("\t{")
			.AppendFormat("\t\t{0} = connectionString;", connectionStringField).AppendLine()
			.AppendLine("\t}")
			.AppendLine();

		// Add methods
		foreach (var (name, fieldName, entityType) in props)
		{
			const string itemVar = "item",
				itemsVar = "items";

			stringBuilder
				.AppendFormat("\tpublic void Add{0}({1} {2}) => {3}.Add({2});", name, entityType, itemVar, fieldName).AppendLine()
				.AppendFormat("\tpublic void Add{0}(IEnumerable<{1}> {2}) => {3}.AddRange({2});", name, entityType, itemsVar, fieldName).AppendLine()
				.AppendLine();
		}

		// Populate method
		stringBuilder
			.AppendLine("\tpublic async Task PopulateAsync(CancellationToken ct = default)")
			.AppendLine("\t{")
			.AppendFormat("\t\tvar {0} = new ProgressBar({1});", progressBarVar, props.Count).AppendLine()
			.AppendFormat("\t\tawait using var {0} = new {1}({2});", databaseVar, typeSymbol, connectionStringField).AppendLine()
			.AppendLine();

		for (var i = 0; i < props.Count; i++)
		{
			const string tempTableVar = "tmp";

			var (propName, fieldName, namedTypeSymbol) = props[i];
			var pkComparison = CreatePkComparison(namedTypeSymbol);

			if (string.IsNullOrEmpty(pkComparison))
			{
				@this.ReportError($"Cannot identify the PK for `{namedTypeSymbol}`", namedTypeSymbol);
				continue;
			}

			if (i != 0)
				stringBuilder.AppendLine();

			stringBuilder
				.AppendFormat("\t\t{0}.Refresh({1}, \"{2}\");", progressBarVar, i, propName).AppendLine()
				.AppendFormat("\t\tif ({0}.Count > 0)", fieldName).AppendLine()
				.AppendLine("\t\t{")
				.AppendFormat("\t\t\tawait using var {0} = await {1}.CreateTempTableAsync({2}, cancellationToken: ct)", tempTableVar, databaseVar, fieldName).AppendLine()
				.AppendLine("\t\t\t\t.ConfigureAwait(false);").AppendLine()
				.AppendFormat("\t\t\tawait {0}.{1}.Merge()", databaseVar, propName).AppendLine()
				.AppendFormat("\t\t\t\t.Using({0})", tempTableVar).AppendLine()
				.AppendLine("\t\t\t\t.OnTargetKey()")
				.AppendLine("\t\t\t\t.InsertWhenNotMatched()")
				.AppendLine("\t\t\t\t.UpdateWhenMatched()")
				.AppendFormat("\t\t\t\t.DeleteWhenNotMatchedBySourceAnd(x => {0}.Any(y => {1}))", tempTableVar, pkComparison).AppendLine()
				.AppendLine("\t\t\t\t.MergeAsync(ct)")
				.AppendLine("\t\t\t\t.ConfigureAwait(false);")
				.AppendLine("\t\t}");
		}

		stringBuilder
			.AppendFormat("\t\t{0}.Refresh({1}, \"{2}: Done!\");", progressBarVar, props.Count, GetPrefixText(className)).AppendLine()
			.AppendLine("\t}");

		stringBuilder
			.AppendLine("}");

		return new SourceGeneratorResultRecord(className, stringBuilder.ToString());
	}

	private static IReadOnlyList<TablePropertyRecord> GetTables(IEnumerable<IPropertySymbol> props)
	{
		var list = new List<TablePropertyRecord>();

		foreach (var prop in props)
		{
			if (prop.Type is not INamedTypeSymbol { TypeArguments.Length: 1, Name: "ITable" } namedType)
				continue;

			var fieldName = prop.Name.CreateFieldName();
			var item = new TablePropertyRecord(prop.Name, fieldName,(INamedTypeSymbol)namedType.TypeArguments[0]);
			list.Add(item);
		}

		return list;
	}

	private static string GetPrefixText(string className)
	{
		var index = -1;
		for (int i = 0, count = 0; i < className.Length; i++)
		{
			if (char.IsUpper(className[i]))
				count++;

			if (count == 2)
			{
				index = i;
				break;
			}
		}

		return index != -1
			? className.Substring(0, index)
			: className;
	}

	private static string CreatePkComparison(ITypeSymbol type)
	{
		var stringBuilder = new StringBuilder();
		var props = type.GetProperties();

		foreach (var prop in props)
		{
			if (!prop.TryGetAttributeValue<bool>("Column", "IsPrimaryKey", out var isPrimaryKey) || !isPrimaryKey)
				continue;

			if (stringBuilder.Length != 0)
				stringBuilder.Append(" && ");

			stringBuilder.AppendFormat("x.{0} == y.{0}", prop.Name);
		}

		return stringBuilder.ToString();
	}
}