using Budgy.Feature.Category;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Budgy;

public sealed partial class SettingsViewModel : BaseViewModel
{

    private readonly ICategoryRepository _categoryRepository;
    [ObservableProperty]
    private bool _isDarkModeEnabled;

    [ObservableProperty]
    private bool _systemThemeEnabled;

    [ObservableProperty]
    private string _themeText;

    public ObservableCollection<Category> Categories { get; } = [];

    public SettingsViewModel(ILogger<SettingsViewModel> logger, ICategoryRepository categoryRepository) : base(logger)
    {
        _categoryRepository = categoryRepository;
        IsDarkModeEnabled = true;
        ThemeText = "Dark Mode";
    }

    [RelayCommand]
    private async Task ShowPage()
    {
        if (SystemThemeEnabled)
        {
            var currentTheme = AppInfo.Current.RequestedTheme;

            IsDarkModeEnabled = currentTheme is AppTheme.Dark;
        }

        await LoadCategories();
    }

    

    [RelayCommand]
    private async Task AddCategory()
    {
        await Shell.Current.GoToAsync(nameof(CategoryPage));
    }


    private async Task LoadCategories()
    {
        var categories = await _categoryRepository.GetCategories();

        foreach (var category in categories)
        {
            Categories.Add(category);
        }
    }

    partial void OnIsDarkModeEnabledChanged(bool value)
    {
        ThemeText = value ? "Dark Mode" : "Light Mode";

        if (IsDarkModeEnabled)
        {
            Application.Current.UserAppTheme = AppTheme.Dark;
        }
        else
        {
            Application.Current.UserAppTheme = AppTheme.Light;
        }
    }


}
