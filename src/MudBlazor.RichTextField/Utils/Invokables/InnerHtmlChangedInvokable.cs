namespace MudBlazor;

internal sealed class InnerHtmlChangedInvokable
{
	private readonly MudRichTextField _richTextField;

	public InnerHtmlChangedInvokable(MudRichTextField richTextField)
	{
		_richTextField = richTextField;
	}

	[JSInvokable("SetValue")]
	public Task SetValueAsync(string innerHtml)
	{
		return _richTextField.SetValueFromInnerHtmlAsync(innerHtml);
	}

	[JSInvokable("SetToolbarOptions")]
	public Task SetToolbarOptionsAsync(ToolbarOptions options)
	{
		return _richTextField.Toolbar?.SetOptionsAsync(options) ?? Task.CompletedTask;
	}
}
