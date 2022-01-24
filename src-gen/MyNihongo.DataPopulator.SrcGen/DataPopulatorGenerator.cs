using MyNihongo.DataPopulator.SrcGen.Utils;

namespace MyNihongo.DataPopulator.SrcGen;

[Generator]
internal sealed class DataPopulatorGenerator : ISourceGenerator
{
	public void Initialize(GeneratorInitializationContext context)
	{
#if DEBUG
		//if (!System.Diagnostics.Debugger.IsAttached)
		//	System.Diagnostics.Debugger.Launch();
#endif

		context.RegisterForSyntaxNotifications(() => new DataPopulatorSyntaxReceiver());
	}

	public void Execute(GeneratorExecutionContext context)
	{
		if (context.SyntaxReceiver is not DataPopulatorSyntaxReceiver syntaxReceiver)
			return;

		foreach (var candidate in syntaxReceiver.Candidates)
		{
			var (hintName, source) = context.GenerateSource(candidate);
			context.AddSource($"{hintName}.g.cs", source);
		}
	}
}