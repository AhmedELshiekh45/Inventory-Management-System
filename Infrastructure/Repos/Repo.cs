using DataModels.Context;
using Domain.InterFaces.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repos
{
    public class Repo<T> : IRepo<T> where T : class
    {
        public Repo(MyContext context)
        {
            Context = context;
            _Set =context.Set<T>();
        }

        private  MyContext Context;
        private DbSet<T> _Set;

        public async Task CreateAsync(T entity)
        {
           await _Set.AddAsync(entity);
          await  Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
          var Entity = await this.GetByIdAsync(id);
            _Set.Remove(Entity);
           await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return await _Set.ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _Set.FindAsync(id);
        }
    }
}
