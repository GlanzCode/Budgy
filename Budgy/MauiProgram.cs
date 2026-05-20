using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Budgy
{
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

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddTransientWithShellRoute<MainPage, MainViewModel>(nameof(MainPage));
            builder.Services.AddTransientWithShellRoute<EntryPage, EntryViewModel>(nameof(EntryPage));
            builder.Services.AddTransientWithShellRoute<SettingsPage, SettingsViewModel>(nameof(SettingsPage));
            builder.Services.AddTransient<IEntryRepository, EntryRepository>();
            builder.Services.AddTransient<ICalculationService, CalculationService>();

            string path = Path.Combine(FileSystem.AppDataDirectory, AppDbContext.DatabaseName);
            builder.Services.AddTransient<AppDbContext>(q => new AppDbContext(path));

            var app =  builder.Build();

            using (var scope = app.Services.CreateAsyncScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreatedAsync();
            }

            return app;
        }
    }
}
