using System;
using MediatR;
using Utilities;

namespace Application.DescribeSticker
{
    public class DescribeStickerCommand : IRequest
    {
        public DescribeStickerCommand(string userId, string stickerId, string description)
        {
            UserId = Guard.Against.Empty(userId, nameof(userId));
            StickerId = Guard.Against.Empty(stickerId, nameof(stickerId));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public string UserId { get; }
        public string StickerId { get; }
        public string Description { get; }
    }
}
