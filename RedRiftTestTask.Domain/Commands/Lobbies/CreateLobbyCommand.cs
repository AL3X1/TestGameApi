using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RedRiftTestTask.Common.Base;
using RedRiftTestTask.Common.Enums;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Common.Models;
using RedRiftTestTask.Domain.Commands.Results.Lobbies;
using RedRiftTestTask.Infrastructure.Entities;
using RedRiftTestTask.Infrastructure.Enums;

namespace RedRiftTestTask.Domain.Commands.Lobbies
{
    public class CreateLobbyCommand : BaseCommand, ICommand<CreateLobbyCommandContext>
    {
        public CreateLobbyCommand(IRuntimeRepository runtimeRepository) : base(runtimeRepository)
        {
        }
        
        public async Task<CommandResult> ExecuteAsync(CreateLobbyCommandContext context)
        {
            CreationResult lobbyCreationResult = new CreationResult();

            try
            {
                Player player = await RuntimeRepository.Query<Player>()
                    .FirstOrDefaultAsync(p => p.Name == context.HostName);
                
                Lobby lobby = new Lobby()
                {
                    Id = Guid.NewGuid(),
                    HostName = context.HostName,
                    PlayerCount = 1,
                    Status = LobbyStatus.Open
                };
                
                await RuntimeRepository.Query<Lobby>().AddAsync(lobby);

                if (player == null)
                {
                    await RuntimeRepository.Query<Player>().AddAsync(new Player()
                    {
                        Id = Guid.NewGuid(),
                        Name = context.HostName,
                        LobbyId = lobby.Id
                    });
                }
                else
                {                    
                    if (player.LobbyId != Guid.Empty)
                    {
                        lobbyCreationResult.StatusCode = CommandResultStatus.Error;
                        lobbyCreationResult.LobbyId = player.LobbyId;
                        return lobbyCreationResult;
                    }
                    
                    player.Health = 10;
                    player.LobbyId = lobby.Id;
                }

                await RuntimeRepository.SaveAsync();
                
                lobbyCreationResult.StatusCode = CommandResultStatus.Ok;
                lobbyCreationResult.HostName = context.HostName;
                lobbyCreationResult.LobbyId = lobby.Id;
            }
            catch (Exception ex)
            {
                lobbyCreationResult.StatusCode = CommandResultStatus.Error;
            }

            return lobbyCreationResult;
        }
    }
}