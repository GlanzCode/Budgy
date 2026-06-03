
using Budgy.Data;
using Budgy.Feature.Statistics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.VisualElements;
using Microsoft.Extensions.Logging;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using EntryTemplate = Budgy.Data.EntryTemplate;

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

    [ObservableProperty]
    private ISeries[] _series = [];

    [ObservableProperty]
    private ISeries[] _incomePerCategorySeries = [];

    [ObservableProperty]
    private double _budgetRatio;

    [ObservableProperty]
    private string _budgetRatioText;



    public ObservableCollection<EntryTemplate> Entries { get; private set; }
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
        CreateExpensePerCategoryChart();
        CreateIncomePerCategoryChart();
    }

    private void CreateIncomePerCategoryChart()
    {
        var grouped = Entries.Where(q => !q.IsExpense).GroupBy(e => e.Category.Id).ToList();

        IncomePerCategorySeries = grouped.Select((group, index) =>
        {
            var color = ParseColor(group.First().Category.Color);

            return (ISeries)new PieSeries<decimal>
            {
                Name = group.First().Category.Name,
                Values = [group.Sum(e => e.Amount)],  // Summe je Kategorie
                Fill = new SolidColorPaint(color),
                InnerRadius = 30,
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                DataLabelsFormatter = point => point.Coordinate.PrimaryValue.ToString("C0"),
            };
        }).ToArray();
    }

    private void CreateExpensePerCategoryChart()
    {
        var grouped = Entries.Where(q => q.IsExpense).GroupBy(e => e.Category.Id).ToList();

        
        int i = 0;
        Series = grouped.Select((group, index) =>
        {
            // Farbe aus dem Model lesen (Hex-String wie "#FF5733")
            var color = ParseColor(group.First().Category.Color);

            return (ISeries)new PieSeries<decimal>
            {
                Name = group.First().Category.Name,
                Values = [group.Sum(e => e.Amount)],  // Summe je Kategorie
                Fill = new SolidColorPaint(color),
                InnerRadius = 30,
                DataLabelsPaint = new SolidColorPaint(SKColors.White),
                DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                DataLabelsFormatter = point => point.Coordinate.PrimaryValue.ToString("C0"),
            };
        }).ToArray();
    }

    private static SKColor ParseColor(string hex)
    {
        // Unterstützt "#RRGGBB" und "#AARRGGBB"
        if (SKColor.TryParse(hex, out var color))
            return color;

        return SKColors.SteelBlue; // Fallback
    }
    private void CalculateData()
    {
        var expense = Entries.Where(q => q.IsExpense).Sum(q => q.Amount);
        var income = Entries.Where(q => !q.IsExpense).Sum(q => q.Amount);

        ExpenseAmount = expense;
        IncomeAmount = income;
        BudgetAmount = _calculationService.CalculateBudget(expense, income);

        BudgetRatio = _calculationService.GetRatio(expense, income);
        BudgetRatioText = GetRatioText(BudgetRatio);
    }

    

    [RelayCommand]
    private async Task AddEntry()
    {
        /*if (BudgetRatio >= 1.0)
            BudgetRatio = 0.0;
        else
            BudgetRatio += 0.1;


        return;*/
        await Shell.Current.GoToAsync(nameof(EntryPage));
    }

    [RelayCommand]
    private async Task Delete(EntryTemplate entry)
    {
        var result = await Shell.Current.DisplayAlertAsync("Confirm Delete", "Are you sure you want to delete this entry?", "Yes", "No");

        if (!result)
            return;

        Entries.Remove(entry);

        await _entryRepository.Delete(entry.Id);
    }

    [RelayCommand]
    private async Task Open(EntryTemplate entry)
    {
        var navigationParameter = new Dictionary<string, object>
        {
            {"entry", entry }
        };

        await Shell.Current.GoToAsync(nameof(EntryPage), navigationParameter);
    }


    private async Task LoadEntries()
    {
        var entries = await _entryRepository.GetEntriesWithCategory();

        Entries.Clear();

        foreach (var entry in entries)
        {
            Entries.Add(entry);
        }
    }
    private string GetRatioText(double ratio)
    {
        double free = 1.0 - ratio;
        if (ratio >= 0.61)
        {
            return $"Kritischer Bereich, das finanzielle Korsett ist eng. {free:P0} deines Einkommens sind frei";
        }

        if (ratio <= 0.4)
        {
            return $"Alles super, sehr flexibel. {free:P0} deines Einkommens sind frei";
        }

        return $"Normalbereich, aber die Fixkosten nehmen Raum ein. {free:P0} deines Einkommens sind frei";
    }

    #region events

    partial void OnBudgetRatioChanged(double oldValue, double newValue)
    {
        BudgetRatioText = GetRatioText(newValue);
    }
    #endregion

}
