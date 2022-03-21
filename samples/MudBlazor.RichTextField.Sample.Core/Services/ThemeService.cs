using System.Reactive.Subjects;

namespace MudBlazor.RichTextField;

internal sealed class ThemeService : IThemeService
{
	private const string IsDarkKey = "HnT";

	private readonly ReplaySubject<bool> _isDarkThemeSubject = new(1);
	private readonly ILocalStorageService _localStorageService;

	private bool _isDark;

	public ThemeService(ILocalStorageService localStorageService)
	{
		_localStorageService = localStorageService;
	}

	public IObservable<bool> IsDarkTheme => _isDarkThemeSubject;

	public async Task InitAsync()
	{
		_isDark = await _localStorageService.GetItemAsync<bool>(IsDarkKey)
			.ConfigureAwait(false);

		PublishTheme(_isDark);
	}

	public async Task ToggleThemeAsync()
	{
		_isDark = !_isDark;

		await _localStorageService.SetItemAsync(IsDarkKey, _isDark)
			.ConfigureAwait(false);

		PublishTheme(_isDark);
	}

	private void PublishTheme(bool isDark)
	{
		_isDarkThemeSubject.OnNext(isDark);
	}
}