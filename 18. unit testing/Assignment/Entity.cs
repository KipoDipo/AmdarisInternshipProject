namespace Assignment
{
    public abstract class Entity<K>
    {
        required public K Id { get; init; }
    }
}
