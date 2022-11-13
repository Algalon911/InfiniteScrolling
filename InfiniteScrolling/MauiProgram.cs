using CommunityToolkit.Maui;
using InfiniteScrolling.ViewModels;
using InfiniteScrolling.Views;
using Microsoft.Extensions.Logging;

namespace InfiniteScrolling;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddSingleton<AnimalListView, AnimalListViewModel>();

		builder.Services.AddSingleton<AnimalService>();


#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
