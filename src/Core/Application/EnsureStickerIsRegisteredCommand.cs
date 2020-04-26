using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using MediatR;

namespace Application
{
    public class EnsureStickerIsRegisteredCommand : IRequest
    {
        public EnsureStickerIsRegisteredCommand(string stickerId)
        {
            StickerId = stickerId;
        }

        public string StickerId { get; }
    }

    public class EnsureStickerIsRegisteredCommandHandler : IRequestHandler<EnsureStickerIsRegisteredCommand>
    {
        private readonly IStickerFinderDbContext dbContext;

        public EnsureStickerIsRegisteredCommandHandler(IStickerFinderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(EnsureStickerIsRegisteredCommand request, CancellationToken ct)
        {
            var sticker = await dbContext.Stickers.FindAsync(request.StickerId);
            if (sticker is null)
            {
                sticker = new Sticker(request.StickerId);
                dbContext.Stickers.Add(sticker);
                await dbContext.SaveChangesAsync(ct);
            }
            return Unit.Value;
        }
    }
}
