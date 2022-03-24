using System.Reactive.Subjects;

namespace MudBlazor.RichTextField;

internal sealed class ThemeService : IThemeService
{
	private const string IsDarkKey = "HnT", IsRtlKey = "NNb";

	private readonly ReplaySubject<bool> _isDarkThemeSubject = new(1), _isRtlSubject = new(1);
	private readonly ILocalStorageService _localStorageService;

	private bool _isDark, _isRtl;

	public ThemeService(ILocalStorageService localStorageService)
	{
		_localStorageService = localStorageService;
	}

	public IObservable<bool> IsDarkTheme => _isDarkThemeSubject;

	public IObservable<bool> IsRightToLeft => _isRtlSubject;

	public async Task InitAsync()
	{
		_isDark = await _localStorageService.GetItemAsync<bool>(IsDarkKey)
			.ConfigureAwait(false);

		_isRtl = await _localStorageService.GetItemAsync<bool>(IsRtlKey)
			.ConfigureAwait(false);

		PublishTheme(_isDark);
		PublishRtl(_isRtl);
	}

	public async Task ToggleThemeAsync()
	{
		_isDark = !_isDark;

		await _localStorageService.SetItemAsync(IsDarkKey, _isDark)
			.ConfigureAwait(false);

		PublishTheme(_isDark);
	}

	public async Task ToggleRightToLeftAsync()
	{
		_isRtl = !_isRtl;

		await _localStorageService.SetItemAsync(IsRtlKey, _isRtl)
			.ConfigureAwait(false);

		PublishRtl(_isRtl);
	}

	private void PublishTheme(bool isDark)
	{
		_isDarkThemeSubject.OnNext(isDark);
	}

	private void PublishRtl(bool isRtl)
	{
		_isRtlSubject.OnNext(isRtl);
	}
}
