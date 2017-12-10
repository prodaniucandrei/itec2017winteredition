using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAppApi.Models;

namespace UniversityAppApi.Repositories.Generic
{
    public interface IGenericRepository<T> where T : BaseModel
    {
        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();
        T Get(int id);
        Task<T> GetAsync(int id);
        T Insert(T entity);
        Task<T> InsertAsync(T entity);
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
        void Delete(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
