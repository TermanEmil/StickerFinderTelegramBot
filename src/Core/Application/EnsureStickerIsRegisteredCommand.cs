using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using MediatR;
using Utilities;

namespace Application
{
    public class EnsureStickerIsRegisteredCommand : IRequest
    {
        public EnsureStickerIsRegisteredCommand(string stickerId, string stickerFileId)
        {
            StickerId = Guard.Against.Empty(stickerId, nameof(stickerId));
            StickerFileId = Guard.Against.Empty(stickerFileId, nameof(stickerFileId)); ;
        }

        public string StickerId { get; }
        public string StickerFileId { get; }
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
                sticker = new Sticker(request.StickerId, request.StickerFileId);
                dbContext.Stickers.Add(sticker);
                await dbContext.SaveChangesAsync(ct);
            }
            return Unit.Value;
        }
    }
}
