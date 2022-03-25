namespace MudBlazor;

public sealed partial class MudRichTextToolbar : ComponentBase
{
	[Parameter]
	public MudRichTextField TextField { get; set; } = default!;
}
