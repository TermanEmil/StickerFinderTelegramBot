namespace Domain
{
    public interface IEntity<out T>
    {
        T Id { get; }
    }
}
