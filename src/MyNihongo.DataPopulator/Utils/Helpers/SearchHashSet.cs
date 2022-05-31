using System.Diagnostics.CodeAnalysis;

namespace MyNihongo.DataPopulator.Utils.Helpers;

public sealed class SearchHashSet : IEnumerable<string>
{
	private readonly HashSet<string> _hashSet = new();
	private readonly ObjectPool<StringBuilder> _stringBuilderPool = new DefaultObjectPoolProvider()
		.CreateStringBuilderPool();

	public void Add(string search)
	{
		const char space = ' ';
		var split = search.Split(space);
		var list = new List<StringBuilder>();

		for (var i = 0; i < split.Length; i++)
		{
			var lastIndex = list.Count - 1;
			var searchTexts = GetSearchTexts(split[i]);

			if (searchTexts.Count > 0 && lastIndex != -1)
			{
				// Create copies of current items starting from the second item
				for (var j = 1; j < searchTexts.Count; j++)
					for (var k = 0; k <= lastIndex; k++)
						list.Add(new StringBuilder(list[k].ToString()));
			}

			var step = list.Count / searchTexts.Count;
			for (var j = 0; j < searchTexts.Count; j++)
			{
				var listStart = step * j;
				var listEnd = listStart + step;
				for (var k = listStart; k < listEnd; k++)
				{
					list[k].Append(' ');
					list[k].Append(searchTexts[j]);
				}

				var currentItem = _stringBuilderPool.Get();
				currentItem.Append(searchTexts[j]);
				list.Add(currentItem);
			}
		}

		for (var i = 0; i < list.Count; i++)
			_hashSet.Add(list[i].ToString());
	}

	public IEnumerator<string> GetEnumerator() =>
		_hashSet
			.OrderByDescending(static x => x.Length)
			.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() =>
		GetEnumerator();

	private static IReadOnlyList<string> GetSearchTexts(in string input)
	{
		var list = new List<string> { input };
		if (TryRemoveChar(input, '\'', out var withoutSingleBracket))
			list.Add(withoutSingleBracket);

		return list;
	}

	private static bool TryRemoveChar(in string input, in char @char, [NotNullWhen(true)] out string? value)
	{
		var index = input.IndexOf(@char);
		if (index == -1)
		{
			value = null;
			return false;
		}

		value = input[..index] + input[(index + 1)..];
		return true;
	}
}
