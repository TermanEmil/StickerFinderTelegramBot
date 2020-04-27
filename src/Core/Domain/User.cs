using Utilities;

namespace Domain
{
    public class User : IEntity<string>
    {
        private User()
        {
        }

        public User(string id)
        {
            Id = Guard.Against.Empty(id, nameof(id));
        }

        public string Id { get; private set; }
    }
}
