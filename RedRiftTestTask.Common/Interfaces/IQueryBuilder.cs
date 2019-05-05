using System.Threading.Tasks;
using RedRiftTestTask.Common.Contexts;

namespace RedRiftTestTask.Common.Interfaces
{
    public interface IQueryBuilder
    {
        Task<TReturnable> QueryAsync<TContext, TReturnable>(TContext context) where TContext : QueryContext;
    }
}