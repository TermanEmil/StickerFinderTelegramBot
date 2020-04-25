using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public static class DbSetExtensions
    {
        public static async Task<T> FindOrThrow<T>(
            this DbSet<T> self,
            object key,
            CancellationToken ct) where T : class
        {
            var obj = await self.FindAsync(new object[] { key }, ct);
            if (obj is null)
                throw new NotFoundException(typeof(T).Name, key);

            return obj;
        }
    }
}
