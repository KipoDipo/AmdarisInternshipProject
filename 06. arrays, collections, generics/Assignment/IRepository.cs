namespace Assignment
{
    public interface IRepository<T, K> where T : Entity<K>
    {
        IEnumerable<T> Read();

        T? ReadById(K id);
        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
