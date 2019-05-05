using System.Threading.Tasks;
using Autofac;
using RedRiftTestTask.Common.Contexts;
using RedRiftTestTask.Common.Interfaces;

namespace RedRiftTestTask.Common.Builders
{
    public class QueryBuilder : IQueryBuilder
    {
        private static IComponentContext _componentContext;

        public QueryBuilder(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }
        
        public async Task<TReturnable> QueryAsync<TContext, TReturnable>(TContext context) where TContext : QueryContext
        {
            return await _componentContext.Resolve<IQuery<TContext, TReturnable>>().QueryAsync(context);
        }
    }
}