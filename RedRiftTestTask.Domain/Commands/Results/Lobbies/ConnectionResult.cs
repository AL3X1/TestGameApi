using RedRiftTestTask.Common.Models;

namespace RedRiftTestTask.Domain.Commands.Results.Lobbies
{
    public class ConnectionResult : CommandResult
    {
        public string Message { get; set; }
    }
}