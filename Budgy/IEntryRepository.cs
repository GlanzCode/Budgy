using System;
using System.Collections.Generic;
using System.Text;

namespace Budgy;

public interface IEntryRepository
{
    Task<IEnumerable<Entry>> GetEntries();

    Task<Entry?> GetEntryById(int id);

    Task<Entry> SaveEntry(Entry entry);

    Task Delete(int id);
}
