using System;
using RedRiftTestTask.Common.Contexts;

namespace RedRiftTestTask.Domain.Queries.Games
{
    public class GetGameByLobbyIdQueryContext : QueryContext
    {
        public Guid LobbyId { get; set; }
    }
}