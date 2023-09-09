using Course.IRepositorios;

namespace Course.Repositorio
{
    public class UserRepository<T> : IUser<T> where T : class
    {
        public Task<T> CreateAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteAsync(T user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T user)
        {
            throw new NotImplementedException();
        }
    }
}
