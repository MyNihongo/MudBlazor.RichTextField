namespace MudBlazor.RichTextField.Tests.Utils.StringExTests;

public sealed class ToValueShould
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData("<br>")]
	[InlineData("<p><br></p>")]
	public void BeEmptyIfEmpty(string? input)
	{
		var result = input.ToValue();

		result
			.Should()
			.BeEmpty();
	}

	[Fact]
	public void NotChangeIfNoTags()
	{
		const string input = nameof(input);

		var result = input.ToValue();

		result
			.Should()
			.Be(input);
	}

	[Fact]
	public void ConvertValueAndParagraph()
	{
		const string input = "input<p>new line</p>";
		var expected = "input" + Environment.NewLine + "new line";

		var result = input.ToValue();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertTwoParagraphs()
	{
		const string input = "<p>input</p><p>new line</p>";
		var expected = "input" + Environment.NewLine + "new line";

		var result = input.ToValue();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertParagraphAndValue()
	{
		const string input = "<p>input</p>new line";
		var expected = "input" + Environment.NewLine + "new line";

		var result = input.ToValue();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertThreeParagraphs()
	{
		const string input = "<p>line1</p><p>line2</p><p>line3</p>";
		var expected = "line1" + Environment.NewLine + "line2" + Environment.NewLine + "line3";

		var result = input.ToValue();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void KeepItalic()
	{
		const string input = "some <i>italic</i> value<p>new <i>line</i></p>";
		var expected = "some <i>italic</i> value" + Environment.NewLine + "new <i>line</i>";

		var result = input.ToValue();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void KeepBold()
	{
		const string input = "some <b>italic</b> value<p>new <b>line</b></p>";
		var expected = "some <b>italic</b> value" + Environment.NewLine + "new <b>line</b>";

		var result = input.ToValue();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertEmptyLine()
	{
		const string input = "<p>line1</p><p><br></p><p>line3</p>";
		var expected = "line1" + Environment.NewLine + Environment.NewLine + "line3";

		var result = input.ToValue();

		result
			.Should()
			.Be(expected);
	}
}
