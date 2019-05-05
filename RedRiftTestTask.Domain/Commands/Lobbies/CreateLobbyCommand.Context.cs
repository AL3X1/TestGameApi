using RedRiftTestTask.Common.Contexts;

namespace RedRiftTestTask.Domain.Commands.Lobbies
{
    public class CreateLobbyCommandContext : CommandContext
    {
        public string HostName { get; set; }
    }
}