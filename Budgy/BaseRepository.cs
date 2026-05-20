namespace Budgy
{
    public abstract class BaseRepository
    {
        protected AppDbContext _db { get; init; }

        protected BaseRepository(AppDbContext db)
        {
            _db = db;
        }
    }
}