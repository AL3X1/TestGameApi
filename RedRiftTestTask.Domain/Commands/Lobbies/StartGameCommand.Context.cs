using System;
using RedRiftTestTask.Common.Contexts;

namespace RedRiftTestTask.Domain.Commands.Lobbies
{
    public class StartGameCommandContext : CommandContext
    {
        public Guid LobbyId { get; set; }
    }
}