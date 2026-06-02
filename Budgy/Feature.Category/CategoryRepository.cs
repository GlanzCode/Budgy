using Budgy.Data;
using Microsoft.EntityFrameworkCore;

namespace Budgy.Feature.Category;

public sealed class CategoryRepository : BaseRepository, ICategoryRepository
{
    public CategoryRepository(AppDbContext dbContext) : base(dbContext)
    {
        
    }

    public async Task Delete(int id)
    {
        var existing = await GetGategoryById(id);

        if (existing is null)
            return;

        _db.Categories.Remove(existing);

        await _db.SaveChangesAsync(); 
    }

    public async Task<IEnumerable<Data.Category>> GetCategories()
    {        
       return await _db.Categories.AsNoTracking().ToListAsync();
    }

    public async Task<Data.Category?> GetGategoryById(int id)
    {
        return await _db.Categories.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<Data.Category> Save(Data.Category category)
    {
        var existing = await GetGategoryById(category.Id);

        if (existing is null)
        {
           await _db.Categories.AddAsync(category);
        }
        else
        {
            _db.Categories.Update(category);
        }

        await _db.SaveChangesAsync();
        return category;
    }
}
