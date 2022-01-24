using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace MyNihongo.DataPopulator.Utils.Extensions;

internal static class StringEx
{
	public static async Task<T> DeserialiseFileAsync<T>(this string filePath, JsonTypeInfo<T>? jsonTypeInfo = null, CancellationToken ct = default)
	{
		await using var fileStream = FileUtils.AsyncStream(filePath);

		var task = jsonTypeInfo != null
			? JsonSerializer.DeserializeAsync(fileStream, jsonTypeInfo, ct)
			: JsonSerializer.DeserializeAsync<T>(fileStream, cancellationToken: ct);

		return await task.ConfigureAwait(false) ?? throw new InvalidOperationException("Cannot deserialise the model");
	}
}