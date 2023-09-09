using Course.IRepositorios;

namespace Course.Repositorio
{
    public class PayrollRepository<T> : IPayroll<T> where T : class
    {
        public Task<T> CreateAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}
