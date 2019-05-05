using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RedRiftTestTask.Common.Base;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Infrastructure.Entities;

namespace RedRiftTestTask.Domain.Queries.Lobbies
{
    public class GetLobbyQuery : BaseQuery, IQuery<GetLobbyQueryContext, Lobby>
    {
        public GetLobbyQuery(IRuntimeRepository runtimeRepository) : base(runtimeRepository)
        {
        }

        public async Task<Lobby> QueryAsync(GetLobbyQueryContext context)
        {
            return await RuntimeRepository.Query<Lobby>().FirstOrDefaultAsync(l => l.Id == context.LobbyId);
        }
    }
}