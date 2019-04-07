using EntityFramework.FluentHelper.Examples.Dao;
using EntityFramework.FluentHelper.Interfaces;

namespace EntityFramework.FluentHelper.Examples.Repositories
{
    public class BaseRepository
    {
        protected IDbContext DbContext { get; set; }

        public BaseRepository()
        {
            DbContext = DaoInitializer.InitializeContext();
        }

        public BaseRepository(IDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
