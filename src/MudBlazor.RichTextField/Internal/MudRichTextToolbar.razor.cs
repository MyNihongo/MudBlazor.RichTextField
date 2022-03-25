namespace MudBlazor.Internal;

public sealed partial class MudRichTextToolbar : ComponentBase
{
	[Parameter]
	public MudRichTextField TextField { get; set; } = default!;

	private bool IsBoldActive { get; set; }

	private bool IsItalicActive { get; set; }
}
