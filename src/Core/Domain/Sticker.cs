using Utilities;

namespace Domain
{
    public class Sticker : IEntity<string>
    {
        private Sticker()
        {
        }

        public Sticker(string id, string fileId)
        {
            Id = Guard.Against.Empty(id, nameof(id));
            FileId = Guard.Against.Empty(fileId, nameof(fileId));
        }

        /// <summary>
        /// sticker.FileUniqueId
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// sticker.FileId
        /// </summary>
        public string FileId { get; private set; }
    }
}
