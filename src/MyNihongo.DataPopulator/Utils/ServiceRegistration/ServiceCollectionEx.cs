using Microsoft.Extensions.DependencyInjection;
using MyNihongo.DataPopulator.Services;

namespace MyNihongo.DataPopulator.Utils.ServiceRegistration;

internal static class ServiceCollectionEx
{
	public static IServiceCollection AddDataPopulator(this IServiceCollection @this)
	{
		LinqToDB.Data.DataConnection.TurnTraceSwitchOn();
		LinqToDB.Data.DataConnection.WriteTraceLine = static (s1, s2, _) =>
		{
			System.Diagnostics.Debug.WriteLine(s1, s2);
		};

		return @this
			.AddSingleton<IClock>(SystemClock.Instance)
			.AddSingleton<IFileSystemService, FileSystemService>()
			.AddKanji();
	}

	private static IServiceCollection AddKanji(this IServiceCollection @this) =>
		@this
			.AddTransient<IKanjiProvider, KanjiProvider>()
			.AddTransient<IKanjiPopulator, KanjiPopulator>()
			.AddTransient<IKanjiDatabaseService, KanjiDatabaseService>();
}