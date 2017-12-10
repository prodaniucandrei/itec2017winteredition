using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAppApi.Auth.Models;
using UniversityAppApi.Models;

namespace UniversityAppApi.Repositories
{
    public class BLLUnitOfWork
    {
        private readonly UniversityContext _ctx;
        private readonly UserManager<UniversityUserModel> _userManager;

        private NoteRepository _noteRepository;
        private SubjectRepository _subjectRepository;
        private StudentRepository _studentRepository;
        private ProfesorRepository _profesorRepository;
        private PrezentaRepository _prezentaRepository;

        public BLLUnitOfWork(UniversityContext ctx, UserManager<UniversityUserModel> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }

        public NoteRepository NoteRepository => _noteRepository ?? (_noteRepository = new NoteRepository(_ctx));
        public SubjectRepository SubjectRepository => _subjectRepository ?? (_subjectRepository = new SubjectRepository(_ctx));
        public StudentRepository StudentRepository => _studentRepository ?? (_studentRepository = new StudentRepository(_ctx));
        public ProfesorRepository ProfesorRepository => _profesorRepository ?? (_profesorRepository = new ProfesorRepository(_ctx));
        public PrezentaRepository PrezentaRepository => _prezentaRepository ?? (_prezentaRepository = new PrezentaRepository(_ctx));
    }
}
