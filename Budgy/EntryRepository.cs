using Budgy.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Entry = Budgy.Data.Entry;

namespace Budgy;

public sealed class EntryRepository : BaseRepository, IEntryRepository
{
    public EntryRepository(AppDbContext db) :base(db)
    {
        
    }

    public async Task Delete(int id)
    {
        var entry = await GetEntryById(id);

        if (entry is null)
            return;
        _db.Entries.Remove(entry);

        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Entry>> GetEntries()
    {
        return await _db.Entries.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Entry>> GetEntriesWithCategory()
    {
        return await _db.Entries.Include(x => x.Category).AsNoTracking().ToListAsync();
    }

    public async Task<Entry?> GetEntryById(int id)
    {
        return await _db.Entries.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Entry> SaveEntry(Entry entry)
    {
        var existing = await GetEntryById(entry.Id);

        if (existing is null)
        {
            await _db.Entries.AddAsync(entry);
        }
        else
        {
            _db.Entries.Update(entry);
        }

        await _db.SaveChangesAsync();
        return entry;
    }
}
