namespace Domain
{
    public class User : IEntity<int>
    {
        private User()
        {
        }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
    }
}
