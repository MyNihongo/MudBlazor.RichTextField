namespace MudBlazor.RichTextField;

public static class Themes
{
	public static readonly MudTheme Light = new()
	{
		Palette = new Palette
		{
			Background = "#f2f2f2",
			BackgroundGrey = "#f2f2f2",
			Surface = "#f2f2f2",
			DrawerBackground = "#e0e0e0",
			OverlayDark = "#cfd1d0"
		}
	};

	public static readonly MudTheme Dark = new()
	{
		Palette = new Palette
		{
			Black = "#27272f",
			Background = "#32333d",
			BackgroundGrey = "#32333d",
			Surface = "#32333d",
			DrawerBackground = "#373740",
			AppbarBackground = "#27272f",
			AppbarText = "rgba(255,255,255, 0.70)",
			TextPrimary = "rgba(255,255,255, 0.70)",
			TextSecondary = "rgba(255,255,255, 0.50)",
			ActionDefault = "#adadb1",
			ActionDisabled = "rgba(255,255,255, 0.26)",
			ActionDisabledBackground = "rgba(255,255,255, 0.12)",
			Divider = "rgba(255,255,255, 0.12)",
			DividerLight = "rgba(255,255,255, 0.06)",
			TableLines = "rgba(255,255,255, 0.12)",
			LinesDefault = "rgba(255,255,255, 0.12)",
			LinesInputs = "rgba(255,255,255, 0.3)",
			TextDisabled = "rgba(255,255,255, 0.2)",
			DrawerText = "rgba(255,255,255, 0.70)"
		}
	};
}