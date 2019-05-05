using System.Threading.Tasks;
using Autofac;
using RedRiftTestTask.Common.Contexts;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Common.Models;

namespace RedRiftTestTask.Common.Builders
{
    public class CommandBuilder : ICommandBuilder
    {
        private static IComponentContext _componentContext;

        public CommandBuilder(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }
        
        public async Task<CommandResult> ExecuteAsync<TContext>(TContext context) where TContext : CommandContext
        {
            return await _componentContext.Resolve<ICommand<TContext>>().ExecuteAsync(context);
        }
    }
}