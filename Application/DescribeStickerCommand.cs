using System;
using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using MediatR;

namespace Api.StartupConfigurations
{
    public class DescribeStickerCommand : IRequest
    {
        public DescribeStickerCommand(int userId, string stickerFileId, string description)
        {
            UserId = userId;
            StickerFileId = stickerFileId;
            Description = description;
        }

        public int UserId { get; }
        public string StickerFileId { get; }
        public string Description { get; }
    }

    public class DescribeStickerCommandHandler : IRequestHandler<DescribeStickerCommand>
    {
        private readonly IStickerFinderContext context;

        public DescribeStickerCommandHandler(IStickerFinderContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DescribeStickerCommand request, CancellationToken ct)
        {
            var author = await context.Users.FindOrThrow(request.UserId, ct);
            var sticker = await context.Stickers.FindOrThrow(request.StickerFileId, ct);

            var descriptions = request.Description.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None);

            foreach (var descriptionStr in descriptions)
            {
                var description = new StickerDescription(author, sticker, descriptionStr);
                context.StickerDescriptions.Add(description);
            }

            await context.SaveChangesAsync(ct);
            return Unit.Value;
        }
    }
}
