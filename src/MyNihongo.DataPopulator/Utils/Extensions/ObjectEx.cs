using System.Text.Json.Serialization.Metadata;

namespace MyNihongo.DataPopulator.Utils.Extensions;

internal static class ObjectEx
{
	public static int GetHashCodeEx<T>(this T @this, JsonTypeInfo<T> jsonTypeInfo) =>
		JsonSerializer.Serialize(@this, jsonTypeInfo)
			.CalculateHashCode();

	private static int CalculateHashCode(this string str)
	{
		unchecked
		{
			var hash1 = (5381 << 16) + 5381;
			var hash2 = hash1;

			for (var i = 0; i < str.Length; i += 2)
			{
				hash1 = ((hash1 << 5) + hash1) ^ str[i];
				if (i == str.Length - 1)
					break;

				hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
			}

			return hash1 + hash2 * 1566083941;
		}
	}
}