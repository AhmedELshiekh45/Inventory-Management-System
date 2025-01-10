using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterFaces.BaseRepository
{
    public interface IRepo<T> where T : class
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);

    }
}
