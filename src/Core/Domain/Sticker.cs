using System;

namespace Domain
{
    public class Sticker : IEntity<string>
    {
        private Sticker()
        {
        }

        public Sticker(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            Id = id;
        }

        public string Id { get; private set; }
    }
}
