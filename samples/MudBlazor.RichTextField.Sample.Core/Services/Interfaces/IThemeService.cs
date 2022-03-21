namespace MudBlazor.RichTextField;

public interface IThemeService
{
	IObservable<bool> IsDarkTheme { get; }

	IObservable<bool> IsRightToLeft { get; }

	Task InitAsync();

	Task ToggleThemeAsync();

	Task ToggleRightToLeftAsync();
}