using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RedRiftTestTask.Common.Base;
using RedRiftTestTask.Common.Interfaces;
using RedRiftTestTask.Infrastructure.Entities;

namespace RedRiftTestTask.Domain.Queries.Games
{
    public class GetGamesQuery : BaseQuery, IQuery<GetGamesQueryContext, List<Game>>
    {
        public GetGamesQuery(IRuntimeRepository runtimeRepository) : base(runtimeRepository)
        {
        }

        public async Task<List<Game>> QueryAsync(GetGamesQueryContext context)
        {
            return await RuntimeRepository.Query<Game>().ToListAsync();
        }
    }
}