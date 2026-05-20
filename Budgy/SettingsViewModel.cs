using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budgy;

public sealed partial class SettingsViewModel : BaseViewModel
{
    [ObservableProperty]
    private bool _isDarkModeEnabled;

    [ObservableProperty]
    private bool _systemThemeEnabled;

    [ObservableProperty]
    private string _themeText;

    public SettingsViewModel(ILogger<SettingsViewModel> logger) : base(logger)
    {
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
