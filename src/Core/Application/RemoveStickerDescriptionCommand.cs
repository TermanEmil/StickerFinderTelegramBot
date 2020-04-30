using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using MediatR;

namespace Application
{
    public class RemoveStickerDescriptionCommand : IRequest
    {
        public RemoveStickerDescriptionCommand(int descriptionId)
        {
            DescriptionId = descriptionId;
        }

        public int DescriptionId { get; }
    }

    public class RemoveStickerDescriptionCommandHandler : IRequestHandler<RemoveStickerDescriptionCommand>
    {
        private readonly IStickerFinderDbContext context;

        public RemoveStickerDescriptionCommandHandler(IStickerFinderDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(RemoveStickerDescriptionCommand command, CancellationToken ct)
        {
            var description = await context.StickerDescriptions.FindOrThrow(command.DescriptionId, ct);
            
            context.StickerDescriptions.Remove(description);
            await context.SaveChangesAsync(ct);

            return Unit.Value;
        }
    }
}
