namespace MudBlazor.RichTextField;

public interface IThemeService
{
	IObservable<bool> IsDarkTheme { get; }

	Task InitAsync();

	Task ToggleThemeAsync();
}