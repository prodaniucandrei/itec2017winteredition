using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityAppApi.Models.Mappings
{
    public class MapConfiguration
    {
        public static void ConfigureStudent(EntityTypeBuilder<StudentModel> m)
        {
            m.HasKey(a => a.Id);
            m.Property(a => a.An).IsRequired();
            m.Property(a => a.Email).IsRequired();
            m.Property(a => a.Facultate).IsRequired();
            m.Property(a => a.Grupa).IsRequired();
            m.Property(a => a.Nume).IsRequired();
            m.Property(a => a.Sectie).IsRequired();
            m.Property(a => a.Subgrupa).IsRequired();
            m.Property(a => a.Telefon).IsRequired();
        }

        public static void ConfigureNote(EntityTypeBuilder<NoteModel> m)
        {
            m.HasKey(a => a.Id);
            m.Property(a => a.Titlu).IsRequired();
            m.Property(a => a.Body).IsRequired();
            m.Property(a => a.StudentId).IsRequired();
            m.Property(a => a.SubjectId).IsRequired();

            m.HasOne(a => a.Student).WithMany(a => a.Notes).HasForeignKey(a => a.StudentId).OnDelete(DeleteBehavior.Restrict);
            m.HasOne(a => a.Subject).WithMany(a => a.Notes).HasForeignKey(a => a.SubjectId).OnDelete(DeleteBehavior.Restrict);
        }

        public static void ConfigurePrezenta(EntityTypeBuilder<PrezentaModel> m)
        {
            m.HasKey(a => a.Id);
            m.Property(a => a.Data).IsRequired();
            m.Property(a => a.StudentId).IsRequired();
            m.Property(a => a.SubjectId).IsRequired();

            m.HasOne(a => a.Student).WithMany(a => a.Prezente).HasForeignKey(a => a.StudentId).OnDelete(DeleteBehavior.Restrict);
            m.HasOne(a => a.Subject).WithMany(a => a.Prezente).HasForeignKey(a => a.SubjectId).OnDelete(DeleteBehavior.Restrict);
        }

        public static void ConfigureSubject(EntityTypeBuilder<SubjectModel> m)
        {
            m.HasKey(a => a.Id);
            m.Property(a => a.Confirmations).IsRequired();
            m.Property(a => a.Date).IsRequired();
            m.Property(a => a.Facultate).IsRequired();
            m.Property(a => a.Locatie).IsRequired();
            m.Property(a => a.Nume).IsRequired();
            m.Property(a => a.Tip).IsRequired();
            m.Property(a => a.StartTime).IsRequired();
            m.Property(a => a.Serie).IsRequired();
            m.Property(a => a.ProfesorId).IsRequired();

            m.HasOne(a => a.Profesor).WithMany(a => a.Subjects).HasForeignKey(a => a.ProfesorId).OnDelete(DeleteBehavior.Restrict);
        }

        public static void ConfigureProfesor(EntityTypeBuilder<ProfesorModel> m)
        {
            m.HasKey(a => a.Id);
            m.Property(a => a.Email).IsRequired();
            m.Property(a => a.Nume).IsRequired();
        }
    }
}
