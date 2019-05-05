using System;
using RedRiftTestTask.Common.Contexts;

namespace RedRiftTestTask.Domain.Commands.Lobbies
{
    public class DisconnectFromLobbyCommandContext : CommandContext
    {
        public Guid LobbyId { get; set; }
        
        public string PlayerName { get; set; }
    }
}