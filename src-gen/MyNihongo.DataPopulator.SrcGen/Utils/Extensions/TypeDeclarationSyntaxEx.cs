using Microsoft.CodeAnalysis.CSharp;

namespace MyNihongo.DataPopulator.SrcGen.Utils.Extensions;

internal static class TypeDeclarationSyntaxEx
{
	public static INamedTypeSymbol GetTypeSymbol(this TypeDeclarationSyntax @this, Compilation compilation) =>
		compilation.GetSemanticModel(@this.SyntaxTree)
			.GetDeclaredSymbol(@this) ?? throw new NullReferenceException($"Type `{@this.Identifier.ValueText}` is not defined");
}