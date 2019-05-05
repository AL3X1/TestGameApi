using System.Threading.Tasks;

namespace RedRiftTestTask.Common.Interfaces
{
    public interface IQuery<in TContext, TReturnable>
    {
        Task<TReturnable> QueryAsync(TContext context);
    }
}