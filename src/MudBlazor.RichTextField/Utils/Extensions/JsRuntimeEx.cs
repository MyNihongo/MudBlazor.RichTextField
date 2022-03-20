namespace MudBlazor;

internal static class JsRuntimeEx
{
	private const string Prefix = "MudBlazorRichTextEdit";

	public static ValueTask InitAsync(this IJSRuntime @this, string elementId) =>
		@this.InvokeVoidAsync($"{Prefix}.init", elementId);

	public static ValueTask UnloadAsync(this IJSRuntime @this, string elementId) =>
		@this.InvokeVoidAsync($"{Prefix}.dispose", elementId);
}