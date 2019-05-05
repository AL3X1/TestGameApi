using System;
using RedRiftTestTask.Common.Contexts;

namespace RedRiftTestTask.Domain.Commands.Lobbies
{
    public class ConnectToLobbyCommandContext : CommandContext
    {
        public Guid LobbyId { get; set; }
        
        public string PlayerName { get; set; }
    }
}