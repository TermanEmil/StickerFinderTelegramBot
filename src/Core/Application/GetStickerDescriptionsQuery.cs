using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class GetStickerDescriptionsQuery : IRequest<IEnumerable<string>>
    {
        public GetStickerDescriptionsQuery(string stickerId)
        {
            StickerId = stickerId ?? throw new ArgumentNullException(nameof(stickerId));
        }

        public string StickerId { get; }
    }

    public class GetStickerDescriptionsQueryHandler : IRequestHandler<GetStickerDescriptionsQuery, IEnumerable<string>>
    {
        private readonly IStickerFinderDbContext context;

        public GetStickerDescriptionsQueryHandler(IStickerFinderDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<string>> Handle(GetStickerDescriptionsQuery request, CancellationToken ct)
        {
            return await context.StickerDescriptions
                .Where(x => x.Sticker.Id == request.StickerId)
                .Select(x => x.Description)
                .ToListAsync(ct);
        }
    }
}
