using Microsoft.Extensions.Logging;
using courseWork.Data;
using courseWork.ViewModel;
using courseWork.Views;
namespace courseWork;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<DatabaseContext>();
		builder.Services.AddSingleton<HikeViewModel>();
        builder.Services.AddSingleton<HikeDetailViewModel>();
        builder.Services.AddSingleton<HikeListPage>();
        builder.Services.AddTransient<AddUpdateHikeDetail>();
        builder.Services.AddTransient<HikeDetailPage>();
        return builder.Build();
	}
}

