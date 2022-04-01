namespace MudBlazor.RichTextField.Tests.Utils.StringExTests;

public sealed class ToInnerHtmlShould
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	public void BeEmptyLineIfEmpty(string? input)
	{
		const string expected = "<p><br></p>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertSingleLine()
	{
		const string input = nameof(input),
			expected = $"<p>{input}</p>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertTwoLines()
	{
		var input = "line1" + Environment.NewLine + "line2";
		const string expected = "<p>line1</p><p>line2</p>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertThreeLines()
	{
		var input = "line1" + Environment.NewLine + "line2" + Environment.NewLine + "line3";
		const string expected = "<p>line1</p><p>line2</p><p>line3</p>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertFirstEmpty()
	{
		var input = Environment.NewLine + "line2" + Environment.NewLine + "line3";
		const string expected = "<p><br></p><p>line2</p><p>line3</p>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertSecondEmpty()
	{
		var input = "line1" + Environment.NewLine + Environment.NewLine + "line3";
		const string expected = "<p>line1</p><p><br></p><p>line3</p>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertThirdEmpty()
	{
		var input = "line1" + Environment.NewLine + "line2" + Environment.NewLine + Environment.NewLine;
		const string expected = "<p>line1</p><p>line2</p><p><br></p>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void KeepItalic()
	{
		var input = "some <i>italic</i> value" + Environment.NewLine + "new <i>line</i>";
		const string expected = "<p>some <i>italic</i> value</p><p>new <i>line</i></p>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void KeepBold()
	{
		var input = "some <b>italic</b> value" + Environment.NewLine + "new <b>line</b>";
		const string expected = "<p>some <b>italic</b> value</p><p>new <b>line</b></p>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}
}
