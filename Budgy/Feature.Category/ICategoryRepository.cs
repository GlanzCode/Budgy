using System;
using System.Collections.Generic;
using System.Text;

namespace Budgy.Feature.Category;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategories();
    Task<Category?> GetGategoryById(int id);
    Task<Category> Save(Category category);
    Task Delete(int id);
}
