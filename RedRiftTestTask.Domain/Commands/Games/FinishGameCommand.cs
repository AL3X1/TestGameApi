using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RedRiftTestTask.Common.Base;
using RedRiftTestTask.Common.Enums;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Common.Models;
using RedRiftTestTask.Infrastructure.Entities;
using RedRiftTestTask.Infrastructure.Enums;

namespace RedRiftTestTask.Domain.Commands.Games
{
    public class FinishGameCommand : BaseCommand, ICommand<FinishGameCommandContext>
    {
        public FinishGameCommand(IRuntimeRepository runtimeRepository) : base(runtimeRepository)
        {
        }

        public async Task<CommandResult> ExecuteAsync(FinishGameCommandContext context)
        {
            CommandResult result = new CommandResult();
            
            try
            {
                Game game = await RuntimeRepository.Query<Game>().FirstOrDefaultAsync(g => g.Id == context.GameId);

                if (game != null)
                {
                    game.WinnerId = context.WinnerId;
                    game.Status = GameStatus.Finished;

                    Lobby lobby = await RuntimeRepository.Query<Lobby>().FirstOrDefaultAsync(l => l.Id == game.LobbyId);

                    lobby.Status = LobbyStatus.Closed;
                    lobby.PlayerCount = 0;

                    List<Player> players = await RuntimeRepository.Query<Player>().Where(p => p.LobbyId == lobby.Id).ToListAsync();

                    foreach (var player in players)
                    {
                        player.LobbyId = Guid.Empty;
                    }

                    await RuntimeRepository.SaveAsync();
                    result.StatusCode = CommandResultStatus.Ok;
                }
            }
            catch (Exception ex)
            {
                result.StatusCode = CommandResultStatus.Error;
            }

            return result;
        }
    }
}