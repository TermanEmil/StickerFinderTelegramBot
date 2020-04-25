using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
