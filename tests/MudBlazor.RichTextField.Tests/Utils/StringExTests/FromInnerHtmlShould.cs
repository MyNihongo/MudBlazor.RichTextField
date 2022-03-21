using Microsoft.AspNetCore.Components;

namespace MudBlazor.RichTextField.Tests.Utils.StringExTests;

public sealed class FromInnerHtmlShould
{
	[Fact]
	public void NotChangeIfNoTags()
	{
		const string inputValue = nameof(inputValue);

		var result = inputValue.ToValue();

		result
			.Should()
			.Be(inputValue);
	}
}
