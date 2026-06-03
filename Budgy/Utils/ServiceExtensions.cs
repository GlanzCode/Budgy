using Budgy.Data;
using Budgy.Feature.Category;
using CommunityToolkit.Maui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budgy.Utils;

public static class ServiceExtensions
{
    extension(IServiceCollection serviceCollection)
    {
        public void AddTransientPages()
        {
            serviceCollection.AddTransientWithShellRoute<MainPage, MainViewModel>(nameof(MainPage));
            serviceCollection.AddTransientWithShellRoute<EntryPage, EntryViewModel>(nameof(EntryPage));
            serviceCollection.AddTransientWithShellRoute<SettingsPage, SettingsViewModel>(nameof(SettingsPage));
            serviceCollection.AddTransientWithShellRoute<CategoryPage, CategoryViewModel>(nameof(CategoryPage));
            serviceCollection.AddTransientWithShellRoute<ListPage, ListViewModel>(nameof(ListPage));
        }

        public void AddRepositories()
        {
            serviceCollection.AddTransient<IEntryRepository, EntryRepository>();
            serviceCollection.AddTransient<ICategoryRepository, CategoryRepository>();
        }

        public void AddServices()
        {
            serviceCollection.AddTransient<ICalculationService, CalculationService>();
        }

        public void AddDbContext()
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, AppDbContext.DatabaseName);
            serviceCollection.AddTransient(q => new AppDbContext() { DbPath = path });
        }
    }
}
