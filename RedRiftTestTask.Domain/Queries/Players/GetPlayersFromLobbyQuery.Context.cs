using System;
using RedRiftTestTask.Common.Contexts;

namespace RedRiftTestTask.Domain.Queries.Players
{
    public class GetPlayersFromLobbyQueryContext : QueryContext
    {
        public Guid LobbyId { get; set; }
    }
}