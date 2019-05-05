using System;
using System.Linq;
using System.Threading.Tasks;
using RedRiftTestTask.Common.Base;
using RedRiftTestTask.Common.Enums;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Common.Models;
using RedRiftTestTask.Domain.Commands.Results.Games;
using RedRiftTestTask.Infrastructure.Entities;
using RedRiftTestTask.Infrastructure.Enums;

namespace RedRiftTestTask.Domain.Commands.Games
{
    public class CreateGameCommand : BaseCommand, ICommand<CreateGameCommandContext>
    {
        public CreateGameCommand(IRuntimeRepository runtimeRepository) : base(runtimeRepository)
        {
        }

        public async Task<CommandResult> ExecuteAsync(CreateGameCommandContext context)
        {
            CreationResult creationResult = new CreationResult();

            try
            {
                Lobby lobby = RuntimeRepository.Query<Lobby>().FirstOrDefault(l => l.Id == context.LobbyId);

                if (lobby == null)
                {
                    creationResult.StatusCode = CommandResultStatus.Error;
                }
                else if (lobby.Status == LobbyStatus.Open)
                {
                    Game game = new Game()
                    {
                        Id = Guid.NewGuid(),
                        LobbyId = lobby.Id,
                        Status = GameStatus.Started
                    };

                    RuntimeRepository.Query<Game>().Add(game);
                    await RuntimeRepository.SaveAsync();
                }
            }
            catch (Exception)
            {
                creationResult.StatusCode = CommandResultStatus.Error;
            }

            return creationResult;
        }
    }
}