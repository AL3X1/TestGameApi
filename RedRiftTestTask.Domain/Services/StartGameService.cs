using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RedRiftTestTask.Common.Enums;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Common.Models;
using RedRiftTestTask.Domain.Commands.Games;
using RedRiftTestTask.Domain.Commands.Lobbies;
using RedRiftTestTask.Domain.Queries.Games;
using RedRiftTestTask.Domain.Queries.Lobbies;
using RedRiftTestTask.Domain.Queries.Players;
using RedRiftTestTask.Infrastructure.Entities;
using RedRiftTestTask.Infrastructure.Enums;

namespace RedRiftTestTask.Domain.Services
{
    public class StartGameService : IHostedService, IDisposable
    {
        private ICommandBuilder _commandBuilder;

        private IQueryBuilder _queryBuilder;

        private IConfiguration _configuration;

        private Timer _timer;

        private ILifetimeScope _lifetimeScope;
        
        public StartGameService(ICommandBuilder commandBuilder, IQueryBuilder queryBuilder, IConfiguration configuration, ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            _commandBuilder = commandBuilder;
            _queryBuilder = queryBuilder;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine("Started Game Service");
            
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
            Debug.WriteLine("Started GameServiceWorker");

            List<Lobby> lobbies =
                await _queryBuilder.QueryAsync<GetLobbiesQueryContext, List<Lobby>>(new GetLobbiesQueryContext());
            int lobbyClientsLimit = Convert.ToInt32(_configuration?.GetSection("LobbyClientsLimit")?.Value ?? "2");

            foreach (Lobby lobby in lobbies)
            {
                Game game = await _queryBuilder.QueryAsync<GetGameByLobbyIdQueryContext, Game>(
                    new GetGameByLobbyIdQueryContext()
                    {
                        LobbyId = lobby.Id
                    });

                if (game == null)
                {
                    if (lobby.PlayerCount >= lobbyClientsLimit
                        && lobby.Status == LobbyStatus.Open)
                    {
                        await _commandBuilder.ExecuteAsync(
                            new CreateGameCommandContext()
                            {
                                LobbyId = lobby.Id
                            });

                        await _commandBuilder.ExecuteAsync(new StartGameCommandContext()
                        {
                            LobbyId = lobby.Id
                        });

                        Debug.WriteLine("Game started.");
                    }
                }
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}