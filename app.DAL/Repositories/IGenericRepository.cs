using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.DAL.Repositories
{
    public interface IGenericRepository<T>  where T : class
    {
        Task<List<T>> GetAllAsync(); 
        Task<T> GetAsync(int id);
        Task<T> Post(T entity);
        Task<T> Update(T entity);   
        Task<T> Delete(int id); 
       
    }
}
