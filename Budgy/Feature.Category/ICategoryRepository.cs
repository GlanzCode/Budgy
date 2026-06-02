using System;
using System.Collections.Generic;
using System.Text;

namespace Budgy.Feature.Category;

public interface ICategoryRepository
{
    Task<IEnumerable<Data.Category>> GetCategories();
    Task<Data.Category?> GetGategoryById(int id);
    Task<Data.Category> Save(Data.Category category);
    Task Delete(int id);
}
