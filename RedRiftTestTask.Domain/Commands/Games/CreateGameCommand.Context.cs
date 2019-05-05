using System;
using RedRiftTestTask.Common.Contexts;

namespace RedRiftTestTask.Domain.Commands.Games
{
    public class CreateGameCommandContext : CommandContext
    {
        public Guid LobbyId { get; set; }
    }
}