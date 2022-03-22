namespace MudBlazor.RichTextField.Tests.Utils.StringExTests;

public sealed class ToValueShould
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData("<br>")]
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
	public void ConvertValueAndDiv()
	{
		const string input = "input<div>new line</div>";
		var expected = "input" + Environment.NewLine + "new line";

		var result = input.ToValue();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertTwoDivs()
	{
		const string input = "<div>input</div><div>new line</div>";
		var expected = "input" + Environment.NewLine + "new line";

		var result = input.ToValue();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertDivAndValue()
	{
		const string input = "<div>input</div>new line";
		var expected = "input" + Environment.NewLine + "new line";

		var result = input.ToValue();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertThreeDivs()
	{
		const string input = "<div>line1</div><div>line2</div><div>line3</div>";
		var expected = "line1" + Environment.NewLine + "line2" + Environment.NewLine + "line3";

		var result = input.ToValue();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void KeepItalic()
	{
		const string input = "some <i>italic</i> value<div>new <i>line</i></div>";
		var expected = "some <i>italic</i> value" + Environment.NewLine + "new <i>line</i>";

		var result = input.ToValue();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void KeepBold()
	{
		const string input = "some <b>italic</b> value<div>new <b>line</b></div>";
		var expected = "some <b>italic</b> value" + Environment.NewLine + "new <b>line</b>";

		var result = input.ToValue();

		result
			.Should()
			.Be(expected);
	}

	[Fact]
	public void ConvertEmptyLine()
	{
		const string input = "<div>line1</div><div><br></div><div>line3</div>";
		var expected = "line1" + Environment.NewLine + Environment.NewLine + "line3";

		var result = input.ToValue();

		result
			.Should()
			.Be(expected);
	}
}
