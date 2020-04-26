namespace Domain
{
    public class StickerDescription : IEntity<int>
    {
        private StickerDescription()
        {
        }

        public StickerDescription(User author, Sticker sticker, string description)
        {
            Author = author;
            Sticker = sticker;
            Description = description;
        }

        public int Id { get; private set; }
        public User Author { get; private set; }
        public Sticker Sticker { get; private set; }
        public string Description { get; set; }
    }
}
