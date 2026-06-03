using Budgy.Data;
using System;
using System.Collections.Generic;
using System.Text;
using EntryTemplate = Budgy.Data.EntryTemplate;

namespace Budgy;

public interface IEntryRepository
{
    Task<IEnumerable<EntryTemplate>> GetEntries();

    Task<EntryTemplate?> GetEntryById(int id);

    Task<EntryTemplate> SaveEntry(EntryTemplate entry);

    Task Delete(int id);
    Task<IEnumerable<EntryTemplate>> GetEntriesWithCategory();
}
