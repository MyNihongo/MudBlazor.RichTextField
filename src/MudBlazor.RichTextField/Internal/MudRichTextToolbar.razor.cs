namespace MudBlazor.Internal;

public sealed partial class MudRichTextToolbar : ComponentBase
{
	private MudRichTextField _textField = default!;

	[Parameter]
	public MudRichTextField TextField
	{
		get => _textField;
		set
		{
			_textField = value;
			_textField.Toolbar = this;
		}
	}

	private bool IsBoldActive { get; set; }

	private bool IsItalicActive { get; set; }

	internal async Task SetOptionsAsync(ToolbarOptions options)
	{
		var stateHasChanged = false;

		if (options.IsBoldActive != IsBoldActive)
		{
			IsBoldActive = options.IsBoldActive;
			stateHasChanged = true;
		}

		if (options.IsItalicActive != IsItalicActive)
		{
			IsItalicActive = options.IsItalicActive;
			stateHasChanged = true;
		}

		if (stateHasChanged)
		{
			await InvokeAsync(StateHasChanged)
				.ConfigureAwait(false);
		}
	}
}
