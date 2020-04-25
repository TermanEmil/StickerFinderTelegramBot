namespace Domain
{
    public class Sticker : IEntity<string>
    {
        private Sticker()
        {
        }

        public Sticker(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}
