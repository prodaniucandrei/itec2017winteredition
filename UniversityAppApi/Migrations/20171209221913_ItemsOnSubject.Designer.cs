using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using UniversityAppApi.Models;
using UniversityAppApi.Models.Enums;

namespace UniversityAppApi.Migrations
{
    [DbContext(typeof(UniversityContext))]
    [Migration("20171209221913_ItemsOnSubject")]
    partial class ItemsOnSubject
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("UniversityAppApi.Auth.Models.UniversityRoleModel", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("UniversityAppApi.Auth.Models.UniversityUserModel", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("UniversityAppApi.Models.NoteModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body")
                        .IsRequired();

                    b.Property<int?>("StudentId")
                        .IsRequired();

                    b.Property<int?>("SubjectId")
                        .IsRequired();

                    b.Property<string>("Titlu")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("UniversityAppApi.Models.PrezentaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<int?>("StudentId")
                        .IsRequired();

                    b.Property<int?>("SubjectId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Prezenta");
                });

            modelBuilder.Entity("UniversityAppApi.Models.ProfesorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Nume")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Profesors");
                });

            modelBuilder.Entity("UniversityAppApi.Models.StudentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("An");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Facultate")
                        .IsRequired();

                    b.Property<string>("Grupa")
                        .IsRequired();

                    b.Property<string>("Nume")
                        .IsRequired();

                    b.Property<string>("Sectie")
                        .IsRequired();

                    b.Property<string>("Subgrupa")
                        .IsRequired();

                    b.Property<string>("Telefon")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("UniversityAppApi.Models.SubjectModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Confirmations");

                    b.Property<int>("Date");

                    b.Property<string>("Facultate")
                        .IsRequired();

                    b.Property<string>("Locatie")
                        .IsRequired();

                    b.Property<string>("Nume")
                        .IsRequired();

                    b.Property<int?>("ProfesorId")
                        .IsRequired();

                    b.Property<string>("Serie")
                        .IsRequired();

                    b.Property<string>("StartTime")
                        .IsRequired();

                    b.Property<int>("Tip");

                    b.HasKey("Id");

                    b.HasIndex("ProfesorId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("UniversityAppApi.Auth.Models.UniversityRoleModel")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("UniversityAppApi.Auth.Models.UniversityUserModel")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("UniversityAppApi.Auth.Models.UniversityUserModel")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("UniversityAppApi.Auth.Models.UniversityRoleModel")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UniversityAppApi.Auth.Models.UniversityUserModel")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UniversityAppApi.Models.NoteModel", b =>
                {
                    b.HasOne("UniversityAppApi.Models.StudentModel", "Student")
                        .WithMany("Notes")
                        .HasForeignKey("StudentId");

                    b.HasOne("UniversityAppApi.Models.SubjectModel", "Subject")
                        .WithMany("Notes")
                        .HasForeignKey("SubjectId");
                });

            modelBuilder.Entity("UniversityAppApi.Models.PrezentaModel", b =>
                {
                    b.HasOne("UniversityAppApi.Models.StudentModel", "Student")
                        .WithMany("Prezente")
                        .HasForeignKey("StudentId");

                    b.HasOne("UniversityAppApi.Models.SubjectModel", "Subject")
                        .WithMany("Prezente")
                        .HasForeignKey("SubjectId");
                });

            modelBuilder.Entity("UniversityAppApi.Models.SubjectModel", b =>
                {
                    b.HasOne("UniversityAppApi.Models.ProfesorModel", "Profesor")
                        .WithMany("Subjects")
                        .HasForeignKey("ProfesorId");
                });
        }
    }
}
