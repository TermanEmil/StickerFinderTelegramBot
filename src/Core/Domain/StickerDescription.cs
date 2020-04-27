using System;
using Utilities;

namespace Domain
{
    public class StickerDescription : IEntity<int>
    {
        private StickerDescription()
        {
        }

        public StickerDescription(User author, Sticker sticker, string description)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Sticker = sticker ?? throw new ArgumentNullException(nameof(sticker));
            Description = Guard.Against.Empty(description, nameof(description));
        }

        public int Id { get; private set; }
        public User Author { get; private set; }
        public Sticker Sticker { get; private set; }
        public string Description { get; private set; }
    }
}
