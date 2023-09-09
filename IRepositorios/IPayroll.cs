namespace Course.IRepositorios
{
    public interface IPayroll<T>
    {
        Task<T> CreateAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<T> DeleteAsync(T item);
        Task<T> GetAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
