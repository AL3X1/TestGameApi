using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Common.Models;
using RedRiftTestTask.Domain.Commands.Games;
using RedRiftTestTask.Domain.Commands.Players;
using RedRiftTestTask.Domain.Queries.Games;
using RedRiftTestTask.Domain.Queries.Lobbies;
using RedRiftTestTask.Domain.Queries.Players;
using RedRiftTestTask.Infrastructure.Entities;
using RedRiftTestTask.Infrastructure.Enums;

namespace RedRiftTestTask.Domain.Services
{
    public class GamingService : IHostedService, IDisposable
    {
        private Timer _timer;
        
        private ICommandBuilder _commandBuilder;

        private IQueryBuilder _queryBuilder;

        public GamingService(ICommandBuilder commandBuilder, IQueryBuilder queryBuilder)
        {
            _commandBuilder = commandBuilder;
            _queryBuilder = queryBuilder;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("Started Gaming service.");
            _timer = new Timer(StartWorker, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer.Change(0, Timeout.Infinite);
            return Task.CompletedTask;
        }

        private async void StartWorker(object state)
        {
            Debug.WriteLine("Started worker.");

            List<Game> games =
                await _queryBuilder.QueryAsync<GetGamesQueryContext, List<Game>>(new GetGamesQueryContext());

            Debug.WriteLine("Received games.");

            Parallel.ForEach(games, async game =>
            {
                while (game.WinnerId == Guid.Empty)
                {
                    var lobby = await _queryBuilder.QueryAsync<GetLobbyQueryContext, Lobby>(
                        new GetLobbyQueryContext()
                        {
                            LobbyId = game.LobbyId
                        });

                    if (game.Status == GameStatus.Started
                        && lobby.Status == LobbyStatus.Gaming)
                    {
                        Debug.WriteLine("Received lobby.");

                        List<Player> players =
                            await _queryBuilder.QueryAsync<GetPlayersFromLobbyQueryContext, List<Player>>(
                                new GetPlayersFromLobbyQueryContext()
                                {
                                    LobbyId = lobby.Id
                                });

                        Debug.WriteLine($"Received players ({players.Count}).");

                        players = players.OrderBy(p => Guid.NewGuid()).ToList();

                        foreach (Player player in players)
                        {
                            if (player.Health <= 0)
                            {
                                Player winner = players.FirstOrDefault(p => p.Health == players.Max(m => m.Health));

                                CommandResult result = await _commandBuilder.ExecuteAsync(new FinishGameCommandContext()
                                {
                                    GameId = game.Id,
                                    WinnerId = winner.Id
                                });

                                Debug.WriteLine(
                                    $"Finished game {game.Id} with status {result.StatusCode.ToString()}. Winner is {winner.Name} ({winner.Health} hp)");
                                break;
                            }

                            await _commandBuilder.ExecuteAsync(new DecreaseHealthCommandContext()
                            {
                                PlayerId = player.Id
                            });
                        }
                    }
                }
            });
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}