using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAppApi.Auth.Models;
using UniversityAppApi.Models.Enums;
using UniversityAppApi.Repositories;

namespace UniversityAppApi.Models
{
    public class UniversityContextSeedData
    {
        private UniversityContext _ctx;
        private UserManager<UniversityUserModel> _userManager;
        private RoleManager<UniversityRoleModel> _roleManager;
        private BLLUnitOfWork _bll;

        public UniversityContextSeedData(UniversityContext ctx, UserManager<UniversityUserModel> userManager,
            RoleManager<UniversityRoleModel> roleManager, BLLUnitOfWork bll)
        {
            _ctx = ctx;
            _userManager = userManager;
            _roleManager = roleManager;
            _bll = bll;
        }

        public async Task EnsureDbCreated()
        {
            await _ctx.Database.MigrateAsync();
        }

        public async Task EnsureSeedData()
        {
            if (_roleManager.Roles.Count() == 0)
            {
                Console.WriteLine("seeding roles ...");

                var userRole = new UniversityRoleModel() { Name = "User" };
                await _roleManager.CreateAsync(userRole);
            }

            if (await _userManager.FindByEmailAsync("user@u.com") == null)
            {
                Console.WriteLine("seeding users ...");
                var userUser = new UniversityUserModel()
                {
                    UserName = "user@u.com",
                    Email = "user@u.com",
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(userUser, "user1234");
                await _userManager.AddToRoleAsync(userUser, "User");

                var newUserStudent = new StudentModel()
                {
                    Nume = "Utilizator",
                    An = 2,
                    Email = "user@u.com",
                    Facultate = "Facultatea de Automatica si Calculatoare",
                    Grupa = "1",
                    Sectie = "CTI",
                    Subgrupa = "1.1",
                    Telefon = "0723676136",
                    UserId = userUser.Id
                };

                await _bll.StudentRepository.InsertAsync(newUserStudent);

                var ghitaUser = new UniversityUserModel()
                {
                    UserName = "g@u.com",
                    Email = "g@u.com",
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(ghitaUser, "ghita1234");
                await _userManager.AddToRoleAsync(ghitaUser, "User");

                var newGhitaStudent = new StudentModel()
                {
                    Nume = "Gheorghe Darle",
                    An = 2,
                    Email = "gdarle@u.com",
                    Facultate = "Facultatea de Automatica si Calculatoare",
                    Grupa = "3",
                    Sectie = "CTI",
                    Subgrupa = "3.1",
                    Telefon = "0723676136",
                    UserId = ghitaUser.Id
                };

                await _bll.StudentRepository.InsertAsync(newGhitaStudent);

                var prodiUser = new UniversityUserModel()
                {
                    UserName = "a@u.com",
                    Email = "a@u.com",
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(prodiUser, "prodi1234");
                await _userManager.AddToRoleAsync(prodiUser, "User");

                var newProdiStudent = new StudentModel()
                {
                    Nume = "Andrei Prodaniuc",
                    An = 2,
                    Email = "aprodaniuc@u.com",
                    Facultate = "Facultatea de Automatica si Calculatoare",
                    Grupa = "3",
                    Sectie = "CTI",
                    Subgrupa = "3.1",
                    Telefon = "0724681188",
                    UserId = prodiUser.Id
                };

                await _bll.StudentRepository.InsertAsync(newProdiStudent); 

                await _ctx.SaveChangesAsync();
            }

            if (!_ctx.Profesors.Any())
            {
                Console.WriteLine("seeding profesors ...");

                var newProf1 = new ProfesorModel()
                {
                    Nume = "Alexandru Popoescu",
                    Email = "apopescu@u.com"
                };
                var newProf2 = new ProfesorModel()
                {
                    Nume = "Vasile Marian",
                    Email = "vmarian@u.com"
                };
                var newProf3 = new ProfesorModel()
                {
                    Nume = "Iulian Popa",
                    Email = "ipopa@u.com"
                };
                var newProf4 = new ProfesorModel()
                {
                    Nume = "Andrei Craciun",
                    Email = "acraciun@u.com"
                };

                await _bll.ProfesorRepository.InsertAsync(newProf1);
                await _bll.ProfesorRepository.InsertAsync(newProf2);
                await _bll.ProfesorRepository.InsertAsync(newProf3);
                await _bll.ProfesorRepository.InsertAsync(newProf4);

                await _ctx.SaveChangesAsync();
            }

            if (!_ctx.Subjects.Any())
            {
                Console.WriteLine("seeding subjects ...");

                var newSubj1 = new SubjectModel()
                {
                    Confirmations = 0,
                    Date = 1,
                    Facultate = "Facultatea de Automatica si Calculatoare",
                    Locatie = "A101",
                    Nume = "Matematici speciale",
                    Serie = "CTI",
                    StartTime = "08:00",
                    ProfesorId = 1,
                    Tip = SubjectTypeEnum.Curs
                };
                var newSubj2 = new SubjectModel()
                {
                    Confirmations = 0,
                    Date = 3,
                    Facultate = "Facultatea de Automatica si Calculatoare",
                    Locatie = "A101",
                    Nume = "Matematici speciale",
                    Serie = "CTI",
                    StartTime = "18:00",
                    ProfesorId = 1,
                    Tip = SubjectTypeEnum.Laborator
                };
                var newSubj3 = new SubjectModel()
                {
                    Confirmations = 0,
                    Date = 2,
                    Facultate = "Facultatea de Automatica si Calculatoare",
                    Locatie = "A101",
                    Nume = "Modelare si Simulare",
                    Serie = "CTI",
                    StartTime = "10:00",
                    ProfesorId = 2,
                    Tip = SubjectTypeEnum.Laborator
                };
                var newSubj4 = new SubjectModel()
                {
                    Confirmations = 0,
                    Date = 2,
                    Facultate = "Facultatea de Automatica si Calculatoare",
                    Locatie = "A101",
                    Nume = "Modelare si Simulare",
                    Serie = "CTI",
                    StartTime = "14:00",
                    ProfesorId = 2,
                    Tip = SubjectTypeEnum.Curs
                };

                await _bll.SubjectRepository.InsertAsync(newSubj1);
                await _bll.SubjectRepository.InsertAsync(newSubj2);
                await _bll.SubjectRepository.InsertAsync(newSubj3);
                await _bll.SubjectRepository.InsertAsync(newSubj4);

                await _ctx.SaveChangesAsync();
            }

            if (!_ctx.Notes.Any())
            {
                Console.WriteLine("seeding notes ...");

                var newNote1 = new NoteModel()
                {
                    Body = "Notita mea numarul 1",
                    StudentId = 2,
                    SubjectId = 1,
                    Titlu = "Notita 1"
                };
                var newNote2 = new NoteModel()
                {
                    Body = "Notita mea numarul 1",
                    StudentId = 3,
                    SubjectId = 2,
                    Titlu = "Notita 1"
                };
                var newNote3 = new NoteModel()
                {
                    Body = "Notita mea numarul 2",
                    StudentId = 2,
                    SubjectId = 2,
                    Titlu = "Notita 2"
                };

                await _bll.NoteRepository.InsertAsync(newNote1);
                await _bll.NoteRepository.InsertAsync(newNote2);
                await _bll.NoteRepository.InsertAsync(newNote3);

                await _ctx.SaveChangesAsync();
            }

            if (!_ctx.Prezenta.Any())
            {
                Console.WriteLine("seeding notes ...");

                var newPrezenta1 = new PrezentaModel()
                {
                    Data = new DateTime(),
                    StudentId = 2,
                    SubjectId = 1,
                };
                var newPrezenta2 = new PrezentaModel()
                {
                    Data = new DateTime(),
                    StudentId = 3,
                    SubjectId = 1,
                };

                await _bll.PrezentaRepository.InsertAsync(newPrezenta1);
                await _bll.PrezentaRepository.InsertAsync(newPrezenta2);

                await _ctx.SaveChangesAsync();
            }
        }
    }
}
