using System.Threading.Tasks;
using RedRiftTestTask.Common.Contexts;
using RedRiftTestTask.Common.Models;

namespace RedRiftTestTask.Common.Interfaces
{
    public interface ICommand<in TContext> where TContext : CommandContext
    {
        Task<CommandResult> ExecuteAsync(TContext context);
    }
}