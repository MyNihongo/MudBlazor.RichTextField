namespace MudBlazor.RichTextField;

public static class ServiceCollectionEx
{
	public static IServiceCollection AddSamplesCore(this IServiceCollection @this) =>
		@this
			.AddBlazoredLocalStorage()
			.AddScoped<IThemeService, ThemeService>();
}
