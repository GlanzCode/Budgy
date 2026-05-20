using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Budgy;

public sealed partial class EntryViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IEntryRepository _entryRepository;

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

    public EntryViewModel(IEntryRepository entryRepository, ILogger<EntryViewModel> logger) : base(logger)
    {
        _entryRepository = entryRepository;
        PayDate = DateTime.Now;
        IsExpense = false;
        ExpenseText = "Income";
    }

    [RelayCommand]
    private async Task Save()
    {
        var entry = new Entry
        {
            Id = CurrentEntry?.Id ?? 0,
            IsExpense = IsExpense,
            Amount = Amount,
            Description = Description,
            PayDate = PayDate,
            Title = Title
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
