using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RedRiftTestTask.Common.Base;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Common.Models;
using RedRiftTestTask.Infrastructure.Entities;

namespace RedRiftTestTask.Domain.Queries.Games
{
    public class GetGameByLobbyIdQuery : BaseQuery, IQuery<GetGameByLobbyIdQueryContext, Game>
    {
        public GetGameByLobbyIdQuery(IRuntimeRepository runtimeRepository) : base(runtimeRepository)
        {
        }

        public async Task<Game> QueryAsync(GetGameByLobbyIdQueryContext context)
        {
            return await RuntimeRepository.Query<Game>().FirstOrDefaultAsync(l => l.LobbyId == context.LobbyId);
        }
    }
}