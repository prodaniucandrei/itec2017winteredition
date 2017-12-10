using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAppApi.Models;
using UniversityAppApi.Repositories.Generic;

namespace UniversityAppApi.Repositories
{
    public class StudentRepository : GenericRepository<StudentModel>
    {
        public StudentRepository(UniversityContext ctx) : base(ctx)
        {
        }

        public override async Task<List<StudentModel>> GetAllAsync()
        {
            var items = await _dbSet
                .Include(a => a.Notes)
                .Include(a => a.Prezente)
                .ToListAsync();
            foreach(var item in items)
            {
                foreach(var i in item.Notes)
                {
                    i.Student = null;
                }
                foreach (var i in item.Prezente)
                {
                    i.Student = null;
                }
            }
            return items;
        }

        public async Task<List<StudentModel>> GetAllAsync(int id)
        {
            var student = await GetAsync(id);
            var items = await _dbSet
                .Where(a => a.Facultate == student.Facultate
                    && a.An == student.An
                    && a.Sectie == student.Sectie)
                .Include(a => a.Notes)
                .Include(a => a.Prezente)
                .ToListAsync();
            foreach (var item in items)
            {
                foreach (var i in item.Notes)
                {
                    i.Student = null;
                }
                foreach (var i in item.Prezente)
                {
                    i.Student = null;
                }
            }
            return items;
        }

        public override async Task<StudentModel> GetAsync(int id)
        {
            var item = await _dbSet.Include(a => a.Notes).Include(a => a.Prezente).FirstOrDefaultAsync(a => a.Id == id);
            if (item != null)
            {
                foreach (var i in item.Notes)
                {
                    i.Subject = null;
                    i.Student = null;
                }
                foreach (var i in item.Prezente)
                {
                    i.Subject = null;
                    i.Student = null;
                }
                return item;
            }
            return null;
        }
    }
}
