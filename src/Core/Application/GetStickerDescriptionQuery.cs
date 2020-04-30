using System.Threading;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class GetStickerDescriptionQuery : IRequest<StickerDescription>
    {
        public GetStickerDescriptionQuery(int descriptionId)
        {
            DescriptionId = descriptionId;
        }

        public int DescriptionId { get; }
    }

    public class GetStickerDescriptionQueryHandler : IRequestHandler<GetStickerDescriptionQuery, StickerDescription>
    {
        private readonly IStickerFinderDbContext context;

        public GetStickerDescriptionQueryHandler(IStickerFinderDbContext context)
        {
            this.context = context;
        }

        public Task<StickerDescription> Handle(GetStickerDescriptionQuery request, CancellationToken ct)
        {
            return context.StickerDescriptions
                .Include(x => x.Author)
                .Include(x => x.Sticker)
                .FindOrThrow(request.DescriptionId, ct);
        }
    }
}
