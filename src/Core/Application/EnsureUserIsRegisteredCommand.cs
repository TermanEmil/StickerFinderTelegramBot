using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using MediatR;

namespace Application
{
    public class EnsureUserIsRegisteredCommand : IRequest
    {
        public EnsureUserIsRegisteredCommand(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }

    public class EnsureUserIsRegisteredCommandHandler : IRequestHandler<EnsureUserIsRegisteredCommand>
    {
        private readonly IStickerFinderDbContext dbContext;

        public EnsureUserIsRegisteredCommandHandler(IStickerFinderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(EnsureUserIsRegisteredCommand request, CancellationToken ct)
        {
            var user = await dbContext.Users.FindAsync(request.Id);
            if (user is null)
            {
                user = new User(request.Id);
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync(ct);
            }
            return Unit.Value;
        }
    }
}
