using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Utilities.Exceptions;
using Utilities.Extensions;

namespace Application.DescribeSticker
{
    public class DescribeStickerCommandHandler : IRequestHandler<DescribeStickerCommand>
    {
        private readonly IStickerFinderDbContext dbContext;

        public DescribeStickerCommandHandler(IStickerFinderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(DescribeStickerCommand request, CancellationToken ct)
        {
            var author = await dbContext.Users.FindOrThrow(request.UserId, ct);
            var sticker = await dbContext.Stickers.FindOrThrow(request.StickerId, ct);

            var descriptions = ExtractDescriptions(request.Description.ToLower()).ToList();
            if (descriptions.IsEmpty())
                throw new ValidationException("Descriptions are empty");
            
            foreach (var description in descriptions)
            {
                if (string.IsNullOrWhiteSpace(description))
                    continue;

                if (await DescriptionExists(author, sticker, description))
                    continue;

                var stickerDescription = new StickerDescription(author, sticker, description);
                await dbContext.StickerDescriptions.AddAsync(stickerDescription, ct);
            }

            await dbContext.SaveChangesAsync(ct);
            return Unit.Value;
        }

        private static IEnumerable<string> ExtractDescriptions(string description)
        {
            return description
                .Split(new[] { "\r\n", "\n", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Distinct();
        }

        private Task<bool> DescriptionExists(User author, Sticker sticker, string description)
        {
            return dbContext.StickerDescriptions.AnyAsync(x =>
                x.Author.Id == author.Id &&
                x.Sticker.Id == sticker.Id &&
                x.Description == description);
        }
    }
}