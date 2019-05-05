using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RedRiftTestTask.Common.Base;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Infrastructure.Entities;

namespace RedRiftTestTask.Domain.Queries.Players
{
    public class GetPlayersFromLobbyQuery : BaseQuery, IQuery<GetPlayersFromLobbyQueryContext, List<Player>>
    {
        public GetPlayersFromLobbyQuery(IRuntimeRepository runtimeRepository) : base(runtimeRepository)
        {
        }

        public async Task<List<Player>> QueryAsync(GetPlayersFromLobbyQueryContext context)
        {
            return await RuntimeRepository.Query<Player>().Where(p => p.LobbyId == context.LobbyId).ToListAsync();
        }
    }
}