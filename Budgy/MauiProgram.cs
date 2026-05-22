using Budgy.Feature.Category;
using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;

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
                .ConfigureSyncfusionToolkit()
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
            builder.Services.AddTransientWithShellRoute<CategoryPage, CategoryViewModel>(nameof(CategoryPage));
            builder.Services.AddTransient<IEntryRepository, EntryRepository>();
            builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
            builder.Services.AddTransient<ICalculationService, CalculationService>();

            string path = Path.Combine(FileSystem.AppDataDirectory, AppDbContext.DatabaseName);
            IServiceCollection serviceCollection = builder.Services.AddTransient(q => new AppDbContext() { DbPath = path });

            var app =  builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }

            return app;
        }
    }
}
