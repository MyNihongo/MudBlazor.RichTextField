namespace MudBlazor;

internal static class StringEx
{
	/// <summary>
	/// Converts the innerHTML to a normal RichText string
	/// </summary>
	public static string FromInnerHtml(this string? @this)
	{
		if (string.IsNullOrEmpty(@this))
			return string.Empty;

		return @this;
	}
}