using System;
using System.Linq;
using System.Threading.Tasks;
using RedRiftTestTask.Common.Base;
using RedRiftTestTask.Common.Enums;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Common.Models;
using RedRiftTestTask.Domain.Commands.Results.Lobbies;
using RedRiftTestTask.Infrastructure.Entities;
using RedRiftTestTask.Infrastructure.Enums;

namespace RedRiftTestTask.Domain.Commands.Lobbies
{
    public class DisconnectFromLobbyCommand : BaseCommand, ICommand<DisconnectFromLobbyCommandContext>
    {
        public DisconnectFromLobbyCommand(IRuntimeRepository runtimeRepository) : base(runtimeRepository)
        {
        }

        public async Task<CommandResult> ExecuteAsync(DisconnectFromLobbyCommandContext context)
        {
            DisconnectionResult disconnectionResult = new DisconnectionResult();

            try
            {
                Lobby lobby = RuntimeRepository.Query<Lobby>().FirstOrDefault(l => l.Id == context.LobbyId);
                Player player = RuntimeRepository.Query<Player>().FirstOrDefault(p => p.Name == context.PlayerName);
                
                if (lobby == null)
                {
                    disconnectionResult.StatusCode = CommandResultStatus.Error;
                    disconnectionResult.Message = "Invalid lobby id. Lobby is not exist.";
                }
                else
                {
                    lobby.PlayerCount--;

                    if (lobby.PlayerCount <= 0)
                    {
                        lobby.Status = LobbyStatus.Closed;
                    }
                    
                    if (player == null || player.LobbyId == Guid.Empty)
                    {
                        disconnectionResult.StatusCode = CommandResultStatus.Error;
                        disconnectionResult.Message = $"Unable to disconnect player {context.PlayerName}. Player is not exist in lobby.";
                    }
                    else
                    {
                        player.LobbyId = Guid.Empty;
                        disconnectionResult.StatusCode = CommandResultStatus.Ok;
                        disconnectionResult.Message = $"Player {player.Name} successfully disconnected from lobby {lobby.Id}.";
                    }

                    await RuntimeRepository.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                disconnectionResult.StatusCode = CommandResultStatus.Error;
                disconnectionResult.Message = ex.Message;
            }

            return disconnectionResult;
        }
    }
}