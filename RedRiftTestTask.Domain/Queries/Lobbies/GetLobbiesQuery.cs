using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RedRiftTestTask.Common.Base;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Infrastructure.Entities;

namespace RedRiftTestTask.Domain.Queries.Lobbies
{
    public class GetLobbiesQuery : BaseQuery, IQuery<GetLobbiesQueryContext, List<Lobby>>
    {
        public GetLobbiesQuery(IRuntimeRepository runtimeRepository) : base(runtimeRepository)
        {
        }

        public async Task<List<Lobby>> QueryAsync(GetLobbiesQueryContext context)
        {
            return await RuntimeRepository.Query<Lobby>().ToListAsync();
        }
    }
}