namespace MyNihongo.DataPopulator.Utils;

internal static class FileUtils
{
	public static FileStream AsyncStream(string path) =>
		new(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, 4086, true);
}