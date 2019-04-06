using EntityFramework.FluentHelper.Interfaces;

namespace EntityFramework.FluentHelper.Common
{
    public class EfDbContextManager
    {
        public static IDbContext GenerateContext()
        {
            return new EfDbContext();
        }
    }
}
