using System;

namespace Domain
{
    public class User : IEntity<string>
    {
        private User()
        {
        }

        public User(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            Id = id;
        }

        public string Id { get; private set; }
    }
}
