using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using MyNihongo.DataPopulator;
using MyNihongo.DataPopulator.Services;
using MyNihongo.DataPopulator.Utils.ServiceRegistration;

await Parser.Default.ParseArguments<Args>(args)
	.WithParsedAsync(async x =>
	{
		await using var services = new ServiceCollection()
			.AddSingleton(x)
			.AddDataPopulator()
			.BuildServiceProvider();

		await services.GetRequiredService<IKanjiPopulator>()
			.PopulateKanjiAsync();
	});
