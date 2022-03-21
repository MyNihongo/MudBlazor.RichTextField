namespace MudBlazor;

internal static class StringEx
{
	private const string Heading = "<div>", Trailing = "</div>", Break = "<br>";

	/// <summary>
	/// Converts the innerHTML to a normal RichText string
	/// </summary>
	public static string ToValue(this string? @this)
	{
		if (string.IsNullOrEmpty(@this) || @this == Break)
			return string.Empty;

		return @this
			.Replace(Heading, string.Empty)
			.Replace(Break, string.Empty)
			.RemoveTrailingDiv()
			.Replace(Trailing, Environment.NewLine);
	}

	public static string ToInnerHtml(this string? @this)
	{
		if (string.IsNullOrEmpty(@this))
			return Break;

		return Heading + @this + Trailing;
	}

	private static string RemoveTrailingDiv(this string @this)
	{
		return @this.EndsWith(Trailing)
			? @this[..^Trailing.Length]
			: @this;
	}
}
