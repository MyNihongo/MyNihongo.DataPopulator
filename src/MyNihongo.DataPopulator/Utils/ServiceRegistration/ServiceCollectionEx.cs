using Microsoft.Extensions.DependencyInjection;
using MyNihongo.DataPopulator.Services.Grammar;
using MyNihongo.DataPopulator.Services.Kanji;

namespace MyNihongo.DataPopulator.Utils.ServiceRegistration;

internal static class ServiceCollectionEx
{
	public static IServiceCollection AddDataPopulator(this IServiceCollection @this)
	{
#if DEBUG
		LinqToDB.Data.DataConnection.TurnTraceSwitchOn();
		LinqToDB.Data.DataConnection.WriteTraceLine = static (s1, s2, _) =>
		{
			System.Diagnostics.Debug.WriteLine(s1, s2);
		};
#endif
		return @this
			.AddSingleton<IClock>(SystemClock.Instance)
			.AddSingleton<IFileSystemService, FileSystemService>()
			.AddKanji()
			.AddGrammar();
	}

	private static IServiceCollection AddKanji(this IServiceCollection @this) =>
		@this
			.AddTransient<IKanjiProvider, KanjiProvider>()
			.AddTransient<IKanjiPopulator, KanjiPopulator>()
			.AddTransient<IKanjiDatabaseService, KanjiDatabaseService>();

	private static IServiceCollection AddGrammar(this IServiceCollection @this) =>
		@this
			.AddTransient<IGrammarProvider, GrammarProvider>()
			.AddTransient<IGrammarPopulator, GrammarPopulator>()
			.AddTransient<IGrammarDatabaseService, GrammarDatabaseService>();
}
