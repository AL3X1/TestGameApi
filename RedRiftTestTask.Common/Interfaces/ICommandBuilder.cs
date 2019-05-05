using System.Threading.Tasks;
using RedRiftTestTask.Common.Contexts;
using RedRiftTestTask.Common.Models;

namespace RedRiftTestTask.Common.Interfaces
{
    public interface ICommandBuilder
    {
        Task<CommandResult> ExecuteAsync<TContext>(TContext context) where TContext : CommandContext;
    }
}