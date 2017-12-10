using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAppApi.Models;
using UniversityAppApi.Repositories.Generic;

namespace UniversityAppApi.Repositories
{
    public class ProfesorRepository: GenericRepository<ProfesorModel>
    {
        public ProfesorRepository(UniversityContext ctx): base(ctx)
        {

        }

        public override async Task<List<ProfesorModel>> GetAllAsync()
        {
            var items = await _dbSet
                .Include(a => a.Subjects)
                .ToListAsync();
            foreach (var item in items)
            {
                foreach (var i in item.Subjects)
                {
                    i.Profesor = null;
                }
            }
            return items;
        }

        public override async Task<ProfesorModel> GetAsync(int id)
        {
            var item = await _dbSet.Include(a => a.Subjects).FirstOrDefaultAsync(a => a.Id == id);
            if (item != null)
            {
                foreach (var i in item.Subjects)
                {
                    i.Profesor = null;
                }
                return item;
            }
            return null;
        }
    }
}
