using MyNihongo.DataPopulator.Utils;

namespace MyNihongo.DataPopulator.Services.Core;

internal sealed class FileSystemService : IFileSystemService
{
	private string? _resourceDirectory;
	private static readonly object Lock = new();

	public string GetResourceDirectory(string directory)
	{
		lock (Lock)
		{
			_resourceDirectory ??= GetResourceDir();
		}

		return Path.Combine(_resourceDirectory, directory);
	}

	public async ValueTask<string> GetContentAsync(string path)
	{
		if (!File.Exists(path))
			return string.Empty;

		await using var stream = FileUtils.AsyncStream(path);
		using var reader = new StreamReader(stream);

		return await reader.ReadToEndAsync()
			.ConfigureAwait(false);
	}

	private static string GetResourceDir()
	{
		var currentDir = AppContext.BaseDirectory;
		var currentDirSpan = currentDir.AsSpan();
		var srcDirSpan = "src".AsSpan();

		for (int iStart = currentDirSpan.Length - 1, iEnd = iStart; iStart >= 0; iStart--)
		{
			if (currentDirSpan[iStart] != Path.DirectorySeparatorChar || iStart == iEnd)
				continue;

			var slice = currentDirSpan[(iStart + 1)..iEnd];
			if (slice.SequenceEqual(srcDirSpan))
				return currentDir[..(iStart + 1)] + "ja-data";

			iEnd = iStart;
		}

		throw new FileNotFoundException("Cannot find the resource directory");
	}
}
