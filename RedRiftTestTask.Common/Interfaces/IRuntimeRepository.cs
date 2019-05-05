using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RedRiftTestTask.Common.Interfaces
{
    public interface IRuntimeRepository
    {
        DbSet<TEntity> Query<TEntity>() where TEntity : class;

        Task SaveAsync();

        void Save();
    }
}