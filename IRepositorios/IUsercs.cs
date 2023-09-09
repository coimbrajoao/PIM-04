namespace Course.IRepositorios
{
    public interface IUser<T>
    {
        Task<T> CreateAsync(T user);
        Task<T> UpdateAsync(T user);
        Task<T> DeleteAsync(T user);
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

    }
}
