using System.Text;
using Microsoft.Extensions.ObjectPool;

namespace MudBlazor;

internal static class StringEx
{
	private const string Heading = "<p>", Trailing = "</p>", Break = "<br>";

	private static readonly ObjectPool<StringBuilder> StringBuilderPool = new DefaultObjectPoolProvider()
		.CreateStringBuilderPool();

	/// <summary>
	/// Converts the innerHTML to a RichText string
	/// </summary>
	public static string ToValue(this string? @this)
	{
		if (string.IsNullOrEmpty(@this) || @this == Break)
			return string.Empty;

		var builder = StringBuilderPool.Get();
		var lineStart = @this.StartsWith(Heading) ? Heading.Length : 0;
		var end = @this.EndsWith(Trailing) ? @this.Length - Trailing.Length : @this.Length;

		for (var i = lineStart; i < end; i++)
		{
			if (StartsWith(@this, Heading, i))
			{
				if (lineStart < i)
					builder.AppendLine(@this[lineStart..i]);

				lineStart = i + Heading.Length;
				i = lineStart - 1;
			}
			else if (StartsWith(@this, Trailing, i))
			{
				builder.AppendLine(@this[lineStart..i]);
				lineStart = i + Trailing.Length;
				i = lineStart - 1;
			}
			else if (StartsWith(@this, Break, i))
			{
				lineStart = i + Break.Length;
				i = lineStart - 1;
			}
		}

		if (lineStart < end)
			builder.Append(@this[lineStart..end]);

		return builder.ToString();
	}

	/// <summary>
	/// Converts the RichText string to an innerHTML
	/// </summary>
	public static string ToInnerHtml(this string? @this)
	{
		if (string.IsNullOrEmpty(@this))
			return Heading + Break + Trailing;

		var builder = StringBuilderPool.Get()
			.Append(Heading);

		var lineStart = 0;
		var newLine = Environment.NewLine;

		for (var i = lineStart; i < @this.Length; i++)
		{
			if (StartsWith(@this, newLine, i))
			{
				if (lineStart == i)
					builder.Append(Break);

				lineStart = i + newLine.Length;
				i = lineStart - 1;

				if (lineStart < @this.Length)
					builder.Append(Trailing).Append(Heading);
			}
			else
			{
				builder.Append(@this[i]);
			}
		}

		return builder.Append(Trailing)
			.ToString();
	}

	private static bool StartsWith(in string @this, in string value, in int start)
	{
		if (start + value.Length > @this.Length)
			return false;

		for (var i = 0; i < value.Length; i++)
			if (value[i] != @this[start + i])
				return false;

		return true;
	}
}
