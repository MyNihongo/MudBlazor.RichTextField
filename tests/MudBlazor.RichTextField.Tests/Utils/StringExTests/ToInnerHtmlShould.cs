namespace MudBlazor.RichTextField.Tests.Utils.StringExTests;

public sealed class ToInnerHtmlShould
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	public void BeEmptyLineIfEmpty(string? input)
	{
		const string expected = "<br>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertSingleLine()
	{
		const string input = nameof(input),
			expected = $"<div>{input}</div>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertTwoLines()
	{
		var input = "line1" + Environment.NewLine + "line2";
		const string expected = "<div>line1</div><div>line2</div>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertThreeLines()
	{
		var input = "line1" + Environment.NewLine + "line2" + Environment.NewLine + "line3";
		const string expected = "<div>line1</div><div>line2</div><div>line3</div>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertFirstEmpty()
	{
		var input = Environment.NewLine + "line2" + Environment.NewLine + "line3";
		const string expected = "<div><br></div><div>line2</div><div>line3</div>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertSecondEmpty()
	{
		var input = "line1" + Environment.NewLine + Environment.NewLine + "line3";
		const string expected = "<div>line1</div><div><br></div><div>line3</div>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertThirdEmpty()
	{
		var input = "line1" + Environment.NewLine + "line2" + Environment.NewLine + Environment.NewLine;
		const string expected = "<div>line1</div><div>line2</div><div><br></div>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void KeepItalic()
	{
		var input = "some <i>italic</i> value" + Environment.NewLine + "new <i>line</i>";
		const string expected = "<div>some <i>italic</i> value</div><div>new <i>line</i></div>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void KeepBold()
	{
		var input = "some <b>italic</b> value" + Environment.NewLine + "new <b>line</b>";
		const string expected = "<div>some <b>italic</b> value</div><div>new <b>line</b></div>";

		var result = input.ToInnerHtml();

		result
			.Should()
			.Be(expected);
	}
}
