using AttendanceSystem.Data;
using Microsoft.Maui.LifecycleEvents;

namespace AttendanceSystem;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
		#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif

		builder.Services.AddSingleton<WeatherForecastService>();

		// Set path to the SQLite database (it will be created if it does not exist)
		var dbPath =
			Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
				@"AttendanceSystem.db");
		// Register WeatherForecastService and the SQLite database
		builder.Services.AddSingleton<IAttendanceService>(
			s => ActivatorUtilities.CreateInstance<IAttendanceService>(s, dbPath));

        return builder.Build();
	}
}
