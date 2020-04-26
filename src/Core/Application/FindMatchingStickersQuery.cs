using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoreLinq.Extensions;

namespace Application
{
    public class FindMatchingStickersQuery : IRequest<IEnumerable<Sticker>>
    {
        public FindMatchingStickersQuery(string description)
        {
            Description = description.ToLower().Trim();
        }

        public string Description { get; }
    }

    public class FindMatchingStickersQueryHandler : IRequestHandler<FindMatchingStickersQuery, IEnumerable<Sticker>>
    {
        private readonly IStickerFinderDbContext dbContext;

        public FindMatchingStickersQueryHandler(IStickerFinderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Sticker>> Handle(FindMatchingStickersQuery request, CancellationToken ct)
        {
            return await dbContext.StickerDescriptions
                .Where(x => x.Description.Contains(request.Description))
                .Select(x => x.Sticker)
                .Distinct()
                .ToListAsync(ct);
        }
    }
}
