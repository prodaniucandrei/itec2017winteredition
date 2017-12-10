using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityAppApi.Auth.Models;
using UniversityAppApi.Helpers;
using UniversityAppApi.Models.Mappings;

namespace UniversityAppApi.Models
{
    public class UniversityContext : IdentityDbContext<UniversityUserModel, UniversityRoleModel, string>
    {
        private IConfigurationRoot _config;

        public UniversityContext(IConfigurationRoot config, DbContextOptions options)
            : base(options)
        {
            _config = config;
        }

        //public DbSet<TestModel> Test { get; set; }
        public DbSet<NoteModel> Notes { get; set; }
        public DbSet<ProfesorModel> Profesors { get; set; }
        public DbSet<SubjectModel> Subjects { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<PrezentaModel> Prezenta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<NoteModel>(MapConfiguration.ConfigureNote);
            modelBuilder.Entity<ProfesorModel>(MapConfiguration.ConfigureProfesor);
            modelBuilder.Entity<SubjectModel>(MapConfiguration.ConfigureSubject);

            modelBuilder.Entity<StudentModel>(MapConfiguration.ConfigureStudent);
            modelBuilder.Entity<PrezentaModel>(MapConfiguration.ConfigurePrezenta);

            //modelBuilder.Entity<UniversityUserModel>().Property(x => x.NormalizedEmail).HasMaxLength(128);
            //modelBuilder.Entity<UniversityUserModel>().Property(x => x.NormalizedUserName).HasMaxLength(128);
            //modelBuilder.Entity<UniversityRoleModel>().Property(x => x.NormalizedName).HasMaxLength(128);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:UniversityContextConnection"]);
            //optionsBuilder.UseMySql(new ConfigHelpers().GetConnectionString(_config));
        }
    }
}