namespace MyNihongo.DataPopulator.Tests.Utils.SearchHashSetTests;

public sealed class AddShould : SearchHashSetTestsBase
{
	[Fact]
	public void AddTextWithoutSpaces()
	{
		const string input = nameof(input);
		var expected = new[] { input };

		var fixture = CreateFixture();
		fixture.Add(input);

		fixture
			.Should()
			.BeEquivalentTo(expected);
	}

	[Fact]
	public void AddTextWithOneSpace()
	{
		const string input = "text1 text2";
		var expected = new[] { input, "text2" };

		var fixture = CreateFixture();
		fixture.Add(input);

		fixture
			.Should()
			.BeEquivalentTo(expected);
	}

	[Fact]
	public void AddTextWithSpaces()
	{
		const string input = "text1 text2 text3";
		var expected = new[] { input, "text2 text3", "text3" };

		var fixture = CreateFixture();
		fixture.Add(input);

		fixture
			.Should()
			.BeEquivalentTo(expected);
	}

	[Fact]
	public void AddWithSingleBracket()
	{
		const string input = "text1 can't";
		var expected = new[] { input, "text1 cant", "can't", "cant" };

		var fixture = CreateFixture();
		fixture.Add(input);

		fixture
			.Should()
			.BeEquivalentTo(expected);
	}

	[Fact]
	public void AddWithSingleBracketMultiple()
	{
		const string input = "text1 can't text2 I'm";
		var expected = new[]
		{
			input,
			"text1 cant text2 I'm",
			"text1 can't text2 Im",
			"text1 cant text2 Im",
			"can't text2 I'm",
			"cant text2 I'm",
			"can't text2 Im",
			"cant text2 Im",
			"text2 I'm",
			"text2 Im",
			"I'm",
			"Im"
		};

		var fixture = CreateFixture();
		fixture.Add(input);

		fixture
			.Should()
			.BeEquivalentTo(expected);
	}
}
