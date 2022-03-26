namespace MudBlazor;

internal static class JsRuntimeEx
{
	private const string Prefix = "MudBlazorRichTextEdit";

	public static ValueTask InitAsync(this IJSRuntime @this, string elementId, DotNetObjectReference<InnerHtmlChangedInvokable> innerHtmlChangedInvokable) =>
		@this.InvokeVoidAsync($"{Prefix}.init", elementId, innerHtmlChangedInvokable);

	public static ValueTask SetInnerHtmlAsync(this IJSRuntime @this, string elementId, string innerHtml) =>
		@this.InvokeVoidAsync($"{Prefix}.setInnerHtml", elementId, innerHtml);

	public static ValueTask UnloadAsync(this IJSRuntime @this, string elementId) =>
		@this.InvokeVoidAsync($"{Prefix}.dispose", elementId);

	public static ValueTask<bool> ApplyBoldFormattingAsync(this IJSRuntime @this, string elementId, bool isActive) =>
		@this.ApplyFormattingAsync(elementId, "B", isActive);

	public static ValueTask<bool> ApplyItalicFormattingAsync(this IJSRuntime @this, string elementId, bool isActive) =>
		@this.ApplyFormattingAsync(elementId, "I", isActive);

	public static ValueTask<bool> ApplyUnderlineFormattingAsync(this IJSRuntime @this, string elementId, bool isActive) =>
		@this.ApplyFormattingAsync(elementId, "U", isActive);

	private static ValueTask<bool> ApplyFormattingAsync(this IJSRuntime @this, string elementId, string formatChar, bool isActive) =>
		@this.InvokeAsync<bool>($"{Prefix}.applyFormatting", elementId, formatChar);
}
