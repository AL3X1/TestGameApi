using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RedRiftTestTask.Common.Base;
using RedRiftTestTask.Common.Enums;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Common.Models;
using RedRiftTestTask.Domain.Commands.Results.Lobbies;
using RedRiftTestTask.Infrastructure.Entities;
using RedRiftTestTask.Infrastructure.Enums;

namespace RedRiftTestTask.Domain.Commands.Lobbies
{
    public class ConnectToLobbyCommand : BaseCommand, ICommand<ConnectToLobbyCommandContext>
    {
        private IConfiguration _configuration;
        
        public ConnectToLobbyCommand(IRuntimeRepository runtimeRepository, IConfiguration configuration) : base(runtimeRepository)
        {
            _configuration = configuration;
        }

        public async Task<CommandResult> ExecuteAsync(ConnectToLobbyCommandContext context)
        {
            ConnectionResult connectionResult = new ConnectionResult();

            try
            {
                int lobbyClientsLimit = Convert.ToInt32(_configuration?.GetSection("LobbyClientsLimit")?.Value ?? "2");
                Lobby lobby = RuntimeRepository.Query<Lobby>().FirstOrDefault(l => l.Id == context.LobbyId);
                                
                if (lobby == null)
                {
                    connectionResult.StatusCode = CommandResultStatus.Error;
                    connectionResult.Message = "Invalid lobby id. Lobby is not exist.";
                }
                else
                {
                    int currentClientsCountInLobby = lobby.PlayerCount;

                    if (currentClientsCountInLobby <= 0)
                    {
                        lobby.Status = LobbyStatus.Closed;
                        connectionResult.StatusCode = CommandResultStatus.Error;
                        connectionResult.Message = "Unable to connect to closed lobby.";
                    }
                    else
                    {
                        Player player = RuntimeRepository.Query<Player>()
                            .FirstOrDefault(p => p.Name == context.PlayerName);

                        if (player != null && player.LobbyId == context.LobbyId)
                        {
                            connectionResult.StatusCode = CommandResultStatus.Error;
                            connectionResult.Message =
                                $"Player {context.PlayerName} already connected to lobby {context.LobbyId}";
                        }
                        else
                        {
                            if (currentClientsCountInLobby >= lobbyClientsLimit)
                            {
                                connectionResult.StatusCode = CommandResultStatus.Error;
                                connectionResult.Message = $"Reached connected clients limit ({lobbyClientsLimit}).";
                            }
                            else
                            {
                                lobby.PlayerCount++;

                                if (player == null)
                                {
                                    player = new Player()
                                    {
                                        Id = Guid.NewGuid(),
                                        LobbyId = context.LobbyId,
                                        Name = context.PlayerName
                                    };

                                    RuntimeRepository.Query<Player>().Add(player);
                                }
                                else
                                {
                                    player.LobbyId = context.LobbyId;
                                    player.Health = 10;
                                }

                                await RuntimeRepository.SaveAsync();

                                connectionResult.StatusCode = CommandResultStatus.Ok;
                                connectionResult.Message =
                                    $"{context.PlayerName} successfully connected to lobby {context.LobbyId}.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                connectionResult.StatusCode = CommandResultStatus.Error;
                connectionResult.Message = ex.Message;
            }

            return connectionResult;
        }
    }
}