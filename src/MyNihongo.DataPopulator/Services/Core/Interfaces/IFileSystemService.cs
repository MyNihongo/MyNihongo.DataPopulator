namespace MyNihongo.DataPopulator.Services.Core;

internal interface IFileSystemService
{
	string GetResourceDirectory(string directory);

	ValueTask<string> GetContentAsync(string path);
}
