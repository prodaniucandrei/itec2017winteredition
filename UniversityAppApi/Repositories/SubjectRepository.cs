using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAppApi.Models;
using UniversityAppApi.Repositories.Generic;

namespace UniversityAppApi.Repositories
{
    public class SubjectRepository : GenericRepository<SubjectModel>
    {
        public SubjectRepository(UniversityContext ctx) : base(ctx)
        {
        }

        public override async Task<List<SubjectModel>> GetAllAsync()
        {
            var items = await _dbSet
                .Include(a => a.Profesor)
                .ToListAsync();
            foreach (var item in items)
            {
                item.Profesor = null;
            }
            return items;
        }

        public async Task<List<SubjectModel>> GetAllAsync(int id, bool isConfirmed)
        {
            var student = _ctx.Students.FirstOrDefault(a => a.Id == id);
            List<SubjectModel> items;
            if (isConfirmed)
            {
                items = await _dbSet
                    .Where(a => a.Facultate == student.Facultate
                        && a.Serie == student.Sectie
                        && a.Confirmations > 0)
                    .Include(a => a.Profesor)
                    .ToListAsync();
            }
            else
            {
                items = await _dbSet
                    .Where(a => a.Facultate == student.Facultate
                        && a.Serie == student.Sectie
                        && a.Confirmations == 0)
                    .Include(a => a.Profesor)
                    .ToListAsync();
            }

            foreach (var item in items)
            {
                item.Profesor = null;
            }
            return items;
        }

        public override async Task<SubjectModel> GetAsync(int id)
        {
            var item =  await _dbSet.Include(a => a.Profesor).FirstOrDefaultAsync(a => a.Id == id);
            if (item != null)
            {
                item.Profesor.Subjects = new List<SubjectModel>();
                return item;
            }
            return null;
        }

        public async Task<SubjectModel> GetAsyncWithoutNavigations(int id)
        {
            var item = await _dbSet.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            if (item != null)
            {
                return item;
            }
            return null;
        }

        public override async Task<SubjectModel> UpdateAsync(SubjectModel entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Param is null");
            }
            var existing = await GetAsync(entity.Id);
            if (existing != null)
            {
                _ctx.Entry(existing).CurrentValues.SetValues(entity);
                _ctx.SaveChanges();
                return existing;
            }
            return null;
        }
    }
}
