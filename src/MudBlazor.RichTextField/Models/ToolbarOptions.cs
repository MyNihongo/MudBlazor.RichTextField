namespace MudBlazor;

internal sealed record ToolbarOptions
{
	public bool IsBoldActive { get; init; }

	public bool IsItalicActive { get; init; }
	
	public bool IsUnderlineActive { get; init; }
}