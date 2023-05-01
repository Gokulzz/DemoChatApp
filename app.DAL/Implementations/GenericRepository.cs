using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using app.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace app.DAL.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly DataContext context;
        public GenericRepository(DataContext context)
        {
            this.context = context;
        }
        public async Task<List<T>> GetAllAsync()
        {
            var getAll = await context.Set<T>().ToListAsync();
            return getAll;

        }
        public async Task<T> GetAsync(int id)
        {
            var findById = await context.Set<T>().FindAsync(id);
            return findById;
        }
        public async Task<T> Post(T entity)
        {
            var add = await context.Set<T>().AddAsync(entity);
            return entity;
        }
        public async Task<T> Update(T entity)
        {
            var update = context.Set<T>().Update(entity);
            return entity;
        }
        public async Task<T> Delete(int id)
        {
            var delete= await context.Set<T>().FindAsync(id);
            context.Set<T>().Remove(delete);
            return delete;
        }

    }
}
