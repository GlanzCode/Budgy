using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Budgy;

public sealed partial class MainViewModel : BaseViewModel
{
    private readonly IEntryRepository _entryRepository;
    private readonly ICalculationService _calculationService;

    [ObservableProperty]
    private decimal _expenseAmount;

    [ObservableProperty]
    private decimal _incomeAmount;

    [ObservableProperty]
    private decimal _budgetAmount;

    public ObservableCollection<Entry> Entries { get; private set; }
    public MainViewModel(ILogger<MainViewModel> logger, IEntryRepository entryRepository, ICalculationService calculationService) : base(logger)
    {
        _entryRepository = entryRepository;
        _calculationService = calculationService;


        Entries = [];
    }

    [RelayCommand]
    private async Task ShowPage()
    {
        await LoadEntries();
        CalculateData();
    }

    private void CalculateData()
    {
        var expense = Entries.Where(q => q.IsExpense).Sum(q => q.Amount);
        var income = Entries.Where(q => !q.IsExpense).Sum(q => q.Amount);

        ExpenseAmount = expense;
        IncomeAmount = income;
        BudgetAmount = _calculationService.CalculateBudget(expense, income);
    }

    [RelayCommand]
    private async Task AddEntry()
    {
        await Shell.Current.GoToAsync(nameof(EntryPage));
    }

    [RelayCommand]
    private async Task Delete(Entry entry)
    {
        var result = await Shell.Current.DisplayAlertAsync("Confirm Delete", "Are you sure you want to delete this entry?", "Yes", "No");

        if (!result)
            return;

        Entries.Remove(entry);

        await _entryRepository.Delete(entry.Id);
    }

    [RelayCommand]
    private async Task Open(Entry entry)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            {"entry", entry }
        };

        await Shell.Current.GoToAsync(nameof(EntryPage), navigationParameter);
    }


    private async Task LoadEntries()
    {
        var entries = await _entryRepository.GetEntries();

        Entries.Clear();

        foreach (var entry in entries)
        {
            Entries.Add(entry);
        }
    }


}
