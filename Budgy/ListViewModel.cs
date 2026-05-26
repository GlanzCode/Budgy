using Budgy.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Entry = Budgy.Data.Entry;

namespace Budgy;

public sealed partial class ListViewModel : BaseViewModel
{
    private readonly IEntryRepository _entryRepository;

    [ObservableProperty]
    private string? _searchText;

    private readonly List<Entry> _entries = [];
    private CancellationTokenSource? _searchCts;
    public ObservableCollection<Entry> Entries { get; } = [];


    public ListViewModel(ILogger<ListViewModel> logger, IEntryRepository entryRepository) : base(logger)
    {
        _entryRepository = entryRepository;
    }

    [RelayCommand]
    private async Task ShowPage()
    {
        await LoadEnties();
    }

    [RelayCommand]
    private async Task DeleteEntry(Entry entry)
    {
        Entries.Remove(entry);

        await _entryRepository.Delete(entry.Id);
    }

    [RelayCommand]
    private async Task OpenEntry(Entry entry)
    {
        var parameters = new Dictionary<string, object>
        {
            {"entry", entry }
        };

        await Shell.Current.GoToAsync(nameof(EntryPage), parameters);
    }

    private async Task LoadEnties()
    {
        var entries = await _entryRepository.GetEntriesWithCategory();

        _entries.AddRange(entries);

        foreach (var item in _entries)
            Entries.Add(item);
    }

    async partial void OnSearchTextChanged(string? value)
    {
        _searchCts?.Cancel();
        _searchCts = new();
        var token = _searchCts.Token;

        try
        {
            await Task.Delay(300, token);

            var filtered = await Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(value))
                    return _entries;

                return _entries
                .Where(e => e.Title.Contains(value, StringComparison.OrdinalIgnoreCase))
                .ToList();
            }, token);

            UpdateVisibleEntries(filtered);
        }
        catch (TaskCanceledException) { }
    }

    private void UpdateVisibleEntries(List<Entry> filtered)
    {

        Entries.Clear();

        foreach (var item in filtered)
            Entries.Add(item);
    }
}
