using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using MediatR;

namespace Application
{
    public class EnsureUserExistsCommand : IRequest
    {
        public EnsureUserExistsCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }

    public class EnsureUserExistsCommandHandler : IRequestHandler<EnsureUserExistsCommand>
    {
        private readonly IStickerFinderContext context;

        public EnsureUserExistsCommandHandler(IStickerFinderContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(EnsureUserExistsCommand request, CancellationToken ct)
        {
            var user = await context.Users.FindAsync(request.Id);
            if (user is null)
            {
                user = new User(request.Id, request.Name);
                context.Users.Add(user);
                await context.SaveChangesAsync(ct);
            }
            return Unit.Value;
        }
    }
}
