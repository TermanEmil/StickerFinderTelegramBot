using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using MediatR;

namespace Application
{
    public class EnsureStickerExistsCommand : IRequest
    {
        public EnsureStickerExistsCommand(string stickerId)
        {
            StickerId = stickerId;
        }

        public string StickerId { get; }
    }

    public class EnsureStickerExistsCommandHandler : IRequestHandler<EnsureStickerExistsCommand>
    {
        private readonly IStickerFinderContext context;

        public EnsureStickerExistsCommandHandler(IStickerFinderContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(EnsureStickerExistsCommand request, CancellationToken ct)
        {
            var sticker = await context.Stickers.FindAsync(request.StickerId);
            if (sticker is null)
            {
                sticker = new Sticker(request.StickerId);
                context.Stickers.Add(sticker);
                await context.SaveChangesAsync(ct);
            }
            return Unit.Value;
        }
    }
}
