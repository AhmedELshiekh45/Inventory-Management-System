using DataModels.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Base_Repos
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly MyContext _context;

        public BaseRepo(MyContext context)
        {
            this._context = context;
        }
        public async Task AddAsync(T item)
        {
          await _context.AddAsync(item);
        }

        public async Task DeleteAsync(string id)
        {
           var Item =await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(Item);
        }

   

        public async Task<IEnumerable<T>> GetAllAsync()
        {
         return await  _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}
