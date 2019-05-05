using System;
using RedRiftTestTask.Common.Contexts;

namespace RedRiftTestTask.Domain.Queries.Lobbies
{
    public class GetLobbyQueryContext : QueryContext
    {
        public Guid LobbyId { get; set; }
    }
}