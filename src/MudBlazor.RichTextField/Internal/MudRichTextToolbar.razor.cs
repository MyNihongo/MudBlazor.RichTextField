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
}
