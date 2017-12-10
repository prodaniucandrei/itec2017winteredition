using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAppApi.Models;
using UniversityAppApi.Repositories.Generic;

namespace UniversityAppApi.Repositories
{
    public class NoteRepository : GenericRepository<NoteModel>
    {
        public NoteRepository(UniversityContext ctx) : base(ctx)
        {
        }

        public override async Task<List<NoteModel>> GetAllAsync()
        {
            var items = await _dbSet
                .Include(a => a.Student)
                .Include(a => a.Subject)
                .ToListAsync();
            foreach (var item in items)
            {
                item.Student.Notes = null;
                item.Student.Prezente = null;
                item.Subject.Notes = null;
                item.Subject.Prezente = null;
                item.Subject.Profesor = null;
            }
            return items;
        }

        public async Task<List<NoteModel>> GetAllBySubject(int id)
        {
            var items = await _dbSet
                .Where(a => a.SubjectId == id)
                .ToListAsync();
            foreach (var item in items)
            {
                if (item.Student != null)
                {
                    item.Student.Notes = null;
                    item.Student.Prezente = null;
                }
                if (item.Subject != null)
                {
                    item.Subject.Notes = null;
                    item.Subject.Prezente = null;
                    item.Subject.Profesor = null;
                }
            }
            return items;
        }

        public override async Task<NoteModel> GetAsync(int id)
        {
            var item = await _dbSet.Include(a => a.Student).Include(a => a.Subject).FirstOrDefaultAsync(a => a.Id == id);
            if (item != null)
            {
                if (item.Student != null)
                {
                    item.Student.Notes = null;
                    item.Student.Prezente = null;
                }
                if (item.Subject != null)
                {
                    item.Subject.Notes = null;
                    item.Subject.Prezente = null;
                    item.Subject.Profesor = null;
                }
                return item;
            }
            return null;
        }
    }
}
