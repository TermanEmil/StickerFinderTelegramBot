using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public static class QueryableExtensions
    {
        public static async Task<TEntity> FindOrThrow<TEntity, TKey>(
            this IQueryable<TEntity> self,
            TKey key,
            CancellationToken ct) where TEntity : class, IEntity<TKey>
        {
            TEntity result;

            if (self is null)
                throw new ArgumentNullException(nameof(self));

            if (self is DbSet<TEntity> dbSet)
                result = await dbSet.FindAsync(new object[] { key }, ct);
            else
                result = await self.FirstOrDefaultAsync(x => x.Id.Equals(key), ct);

            return result ?? throw new NotFoundException(typeof(TEntity).Name, key);
        }
    }
}
