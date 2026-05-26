using Budgy.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Entry = Budgy.Data.Entry;

namespace Budgy;

public interface IEntryRepository
{
    Task<IEnumerable<Entry>> GetEntries();

    Task<Entry?> GetEntryById(int id);

    Task<Entry> SaveEntry(Entry entry);

    Task Delete(int id);
    Task<IEnumerable<Entry>> GetEntriesWithCategory();
}
