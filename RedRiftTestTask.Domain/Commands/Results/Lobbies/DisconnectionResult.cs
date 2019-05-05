using RedRiftTestTask.Common.Models;

namespace RedRiftTestTask.Domain.Commands.Results.Lobbies
{
    public class DisconnectionResult : CommandResult
    {
        public string Message { get; set; }
    }
}