using System;
using RedRiftTestTask.Common.Models;

namespace RedRiftTestTask.Domain.Commands.Results.Lobbies
{
    public class CreationResult : CommandResult
    {
        public Guid LobbyId { get; set; }
        
        public string HostName { get; set; }
    }
}