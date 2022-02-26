using FluentHelper.EntityFramework.Interfaces;

namespace FluentHelper.EntityFramework.Common
{
    public class EfDbContextManager
    {
        public static IDbContext GenerateContext()
        {
            return new EfDbContext();
        }
    }
}
