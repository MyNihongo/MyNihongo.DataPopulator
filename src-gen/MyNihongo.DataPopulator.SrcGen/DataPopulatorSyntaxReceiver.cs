namespace MyNihongo.DataPopulator.SrcGen;

internal sealed class DataPopulatorSyntaxReceiver : ISyntaxReceiver
{
	private readonly List<ClassDeclarationSyntax> _candidates = new();

	public IReadOnlyList<ClassDeclarationSyntax> Candidates => _candidates;

	public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
	{
		if (syntaxNode is not ClassDeclarationSyntax classSyntax || classSyntax.BaseList == null)
			return;

		foreach (var type in classSyntax.BaseList.Types)
			if (type.Type is IdentifierNameSyntax { Identifier.ValueText: "DatabaseBase" })
				_candidates.Add(classSyntax);
	}
}