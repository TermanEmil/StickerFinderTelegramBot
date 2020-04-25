using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class DescribeStickerCommand : IRequest
    {
        public DescribeStickerCommand(int userId, string stickerId, string description)
        {
            UserId = userId;
            StickerId = stickerId;
            Description = description;
        }

        public int UserId { get; }
        public string StickerId { get; }
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
            var sticker = await context.Stickers.FindOrThrow(request.StickerId, ct);

            var descriptions = request.Description
                .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
                .Select(x => x.ToLower().Trim());

            foreach (var descriptionStr in descriptions)
            {
                if (string.IsNullOrWhiteSpace(descriptionStr))
                    continue;

                if (await DescriptionExists(author, sticker, descriptionStr))
                    continue;

                var description = new StickerDescription(author, sticker, descriptionStr);
                context.StickerDescriptions.Add(description);
            }

            await context.SaveChangesAsync(ct);
            return Unit.Value;
        }

        private Task<bool> DescriptionExists(User author, Sticker sticker, string description)
        {
            return context.StickerDescriptions.AnyAsync(x =>
                x.Author.Id == author.Id &&
                x.Sticker.Id == sticker.Id &&
                x.Description == description);
        }
    }
}
