using Budgy.Data;
using Budgy.Feature.Category;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Entry = Budgy.Data.Entry;

namespace Budgy;

public sealed partial class EntryViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IEntryRepository _entryRepository;
    private readonly ICategoryRepository _categoryRepository;

    [ObservableProperty]
    private string _title;
    
    [ObservableProperty]
    private string? _description;
    
    [ObservableProperty]
    private decimal _amount;
    
    [ObservableProperty]
    private DateTime? _payDate;
    
    [ObservableProperty]
    private bool _isExpense;

    [ObservableProperty]
    private string _expenseText;

    [ObservableProperty]
    private Category? _selectedCategory;

    public ObservableCollection<Category> Categories { get; } = [];



    public Entry CurrentEntry
    {
        get;
        set
        {
            IsExpense = value.IsExpense;
            PayDate = value.PayDate;
            Title = value.Title;
            Description = value.Description;
            Amount = value.Amount;

            field = value;
        }
    }

    public EntryViewModel(IEntryRepository entryRepository, ILogger<EntryViewModel> logger, ICategoryRepository categoryRepository) : base(logger)
    {
        _entryRepository = entryRepository;
        PayDate = DateTime.Now;
        IsExpense = false;
        ExpenseText = "Income";
        _categoryRepository = categoryRepository;
    }

    [RelayCommand]
    private async Task Save()
    {
        if (!IsValid())
            return;
        var entry = new Entry
        {
            Id = CurrentEntry?.Id ?? 0,
            IsExpense = IsExpense,
            Amount = Amount,
            Description = Description,
            PayDate = PayDate,
            Title = Title,
            CategoryId = SelectedCategory?.Id,
        };

        try
        {
            await _entryRepository.SaveEntry(entry);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving entry");
        }      


        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task ShowPage()
    {
        await LoadCategories();
    }

    [RelayCommand]
    private void ClearSelectedCategory()
    {
        SelectedCategory = null;
    }

    [RelayCommand]
    private void ClearDate()
    {
        PayDate = null;
    }
    private async Task LoadCategories()
    {
        var categories = await _categoryRepository.GetCategories();

        foreach (var category in categories)
        {
            Categories.Add(category);
        }
    }

    private bool IsValid()
    {
        bool isValid = Amount > 0.0m;
        isValid &= !string.IsNullOrWhiteSpace(Title);

        return isValid;
    }

    partial void OnIsExpenseChanged(bool value)
    {
        ExpenseText = value ? "Expense" : "Income";
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("entry", out var entry))
        {
            CurrentEntry = (Entry)entry;
        }
    }
}
