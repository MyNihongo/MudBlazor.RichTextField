namespace MudBlazor.RichTextField.Tests.Utils.StringExTests;

public sealed class FromInnerHtmlShould
{
	[Fact]
	public void NotChangeIfNoTags()
	{
		const string inputValue = nameof(inputValue);

		var result = inputValue.FromInnerHtml();

		result
			.Should()
			.Be(inputValue);
	}
}
