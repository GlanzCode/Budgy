using Budgy.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using EntryTemplate = Budgy.Data.EntryTemplate;

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

    public async Task<IEnumerable<EntryTemplate>> GetEntries()
    {
        return await _db.Entries.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<EntryTemplate>> GetEntriesWithCategory()
    {
        return await _db.Entries.Include(x => x.Category).AsNoTracking().ToListAsync();
    }

    public async Task<EntryTemplate?> GetEntryById(int id)
    {
        return await _db.Entries.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<EntryTemplate> SaveEntry(EntryTemplate entry)
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
