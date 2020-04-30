using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class GetStickerDescriptionsQuery : IRequest<IEnumerable<StickerDescription>>
    {
        public GetStickerDescriptionsQuery(string stickerId)
        {
            StickerId = stickerId ?? throw new ArgumentNullException(nameof(stickerId));
        }

        public string StickerId { get; }
    }

    public class GetStickerDescriptionsQueryHandler :
        IRequestHandler<GetStickerDescriptionsQuery, IEnumerable<StickerDescription>>
    {
        private readonly IStickerFinderDbContext context;

        public GetStickerDescriptionsQueryHandler(IStickerFinderDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<StickerDescription>> Handle(GetStickerDescriptionsQuery request, CancellationToken ct)
        {
            return await context.StickerDescriptions
                .Where(x => x.Sticker.Id == request.StickerId)
                .ToListAsync(ct);
        }
    }
}
