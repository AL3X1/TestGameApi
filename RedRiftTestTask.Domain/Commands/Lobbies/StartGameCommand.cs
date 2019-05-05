using System;
using System.Linq;
using System.Threading.Tasks;
using RedRiftTestTask.Common.Base;
using RedRiftTestTask.Common.Enums;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Common.Models;
using RedRiftTestTask.Infrastructure.Entities;
using RedRiftTestTask.Infrastructure.Enums;

namespace RedRiftTestTask.Domain.Commands.Lobbies
{
    public class StartGameCommand : BaseCommand, ICommand<StartGameCommandContext>
    {
        public StartGameCommand(IRuntimeRepository runtimeRepository) : base(runtimeRepository)
        {
        }

        public async Task<CommandResult> ExecuteAsync(StartGameCommandContext context)
        {
            CommandResult result = new CommandResult();

            var lobby = RuntimeRepository.Query<Lobby>().FirstOrDefault(l => l.Id == context.LobbyId);

            if (lobby != null)
            {
                lobby.Status = LobbyStatus.Gaming;
                await RuntimeRepository.SaveAsync();
            }
            
            result.StatusCode = CommandResultStatus.Ok;
            return result;
        }
    }
}