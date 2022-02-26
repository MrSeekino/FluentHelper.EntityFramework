using FluentHelper.EntityFramework.Examples.Dao;
using FluentHelper.EntityFramework.Interfaces;

namespace FluentHelper.EntityFramework.Examples.Repositories
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
