using Budgy.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budgy.Feature.Category;

public sealed partial class CategoryViewModel : BaseViewModel, IQueryAttributable
{
    private readonly ICategoryRepository _categoryRepository;

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _colorHex;

    public Data.Category CurrentCategory
    {
        get;
        set
        {
            Name = value.Name;
            ColorHex = value.Color;

            field = value;
        }
    }

    public CategoryViewModel(ILogger<CategoryViewModel> logger, ICategoryRepository categoryRepository) : base(logger)
    {
        ColorHex = Colors.DarkBlue.ToRgbaHex();
        _categoryRepository = categoryRepository;
    }

    [RelayCommand]
    private async Task Save()
    {
        if (!IsValid())
            return;

        var category = new Data.Category()
        {
            Id = CurrentCategory?.Id ?? 0,
            Name = Name,
            Color = ColorHex
        };

        try
        {
            await _categoryRepository.Save(category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving category");
        }

        await Shell.Current.GoToAsync("..");
    }

    private bool IsValid()
    {
        bool isValid = !string.IsNullOrWhiteSpace(Name);

        return isValid;
    }

    [RelayCommand]
    private async Task SelectedColor()
    {
        Color selectedColor = Colors.DarkBlue;

#if ANDROID
        selectedColor = await OpenColorPicker();
#endif

        ColorHex = selectedColor.ToRgbaHex();
    }

#if ANDROID
    private async Task<Color> OpenColorPicker()
    {
        var colorConfig = new Docutain.ColorPickerConfig()
        {
            SupportsAlpha = false,
            Title = "Kategorien Farbe auswählen"
        };

        return await Docutain.ColorPicker.PickColor(colorConfig);
    }

#endif
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("category", out var category))
            CurrentCategory = (Data.Category)category;

    }
}
