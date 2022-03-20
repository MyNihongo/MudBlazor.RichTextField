namespace MudBlazor;

internal static class StringEx
{
	private const string Trailing = "</div>", Break = "<br>";

	/// <summary>
	/// Converts the innerHTML to a normal RichText string
	/// </summary>
	public static string FromInnerHtml(this string? @this)
	{
		if (string.IsNullOrEmpty(@this) || @this == Break)
			return string.Empty;

		return @this
			.Replace("<div>", string.Empty)
			.Replace($"{Break}{Trailing}", Environment.NewLine)
			.RemoveTrailingDiv()
			.Replace(Trailing, Environment.NewLine);
	}

	private static string RemoveTrailingDiv(this string @this)
	{
		return @this.EndsWith(Trailing)
			? @this[..^Trailing.Length]
			: @this;
	}
}
