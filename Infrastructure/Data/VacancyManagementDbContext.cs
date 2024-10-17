using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Common.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using Domain.Views;
using Common.Queries;
using Common.Queries;
using static CSharpFunctionalExtensions.Result;

namespace Infrastructure.Data
{
    public class VacancyManagementDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public VacancyManagementDbContext()
        {
        }
        public VacancyManagementDbContext(DbContextOptions<VacancyManagementDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }


        public DbSet<Vacancy> Vacancy { get; set; }
        public DbSet<VacancyGroup> VacancyGroup { get; set; }
        public DbSet<Candidate> Candidate { get; set; }
        public DbSet<Domain.Entities.File> File { get; set; }
        public DbSet<AnswerOption> AnswerOption { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<CandidateAnswer> CandidateAnswer { get; set; }
        public DbSet<CandidateVacancy> CandidateVacancy { get; set; }
        public DbSet<VwVacancy> VwVacancy { get; set; }
        public DbSet<VwCandidate> VwCandidate { get; set; }
        public DbSet<VwCandidateAnswers> VwCandidateAnswers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {//    var connectionString = _configuration.GetConnectionString("SqlServerConnection1");
            optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=VacancyManagementDb;Integrated Security=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<AuditableEntity>();
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType) && entityType.ClrType != typeof(AuditableEntity))
                {
                    modelBuilder.Entity(entityType.ClrType).Property("CreatedAt")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    modelBuilder.Entity(entityType.ClrType).Property("IsDeleted")
                      .HasDefaultValue(false);
                }
            }

            modelBuilder.Entity<Vacancy>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Description).IsRequired();

                entity.HasOne(v => v.VacancyGroup)
                .WithMany(vg => vg.Vacancies)
                .HasForeignKey(v => v.VacancyGroupId);

            });

            modelBuilder.Entity<VacancyGroup>(entity =>
            {
                entity.HasKey(e => e.Id);

            });

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Surname).IsRequired();
                entity.Property(e => e.Email).IsRequired();
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(q => q.Id);
                entity.Property(q => q.QuestionText).IsRequired();
                entity.Property(q => q.DifficultyLevel).IsRequired();

                entity.HasOne(v => v.Vacancy)
                .WithMany(vg => vg.Questions)
                .HasForeignKey(v => v.VacancyId);
            });


            modelBuilder.Entity<AnswerOption>(entity =>
            {
                entity.HasKey(q => q.Id);
                entity.Property(q => q.AnswerText).IsRequired();
                entity.HasOne(a => a.Question)
                .WithMany(q => q.AnswerOptions)
                .HasForeignKey(a => a.QuestionId);
            });


            modelBuilder.Entity<CandidateAnswer>(entity =>
            {
                entity.HasKey(ca => ca.Id);
                entity.Property(r => r.CandidateId).IsRequired();
                entity.Property(r => r.AnswerOptionId).IsRequired();
                entity.Property(r => r.QuestionId).IsRequired();
                entity.Property(r => r.VacancyId).IsRequired();
                entity.HasOne(ca => ca.Candidate)
                    .WithMany(c => c.CandidateAnswers)
                    .HasForeignKey(ca => ca.CandidateId);

                entity.HasOne(ca => ca.AnswerOption)
                    .WithMany(ao => ao.CandidateAnswers)
                    .HasForeignKey(ca => ca.AnswerOptionId);

                entity.HasOne(ca => ca.Question)
                  .WithMany(ao => ao.CandidateAnswers)
                  .HasForeignKey(ca => ca.QuestionId)
                  .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ca => ca.Vacancy)
                  .WithMany(ao => ao.CandidateAnswers)
                  .HasForeignKey(ca => ca.VacancyId)
                  .OnDelete(DeleteBehavior.Restrict); ;
            });

            modelBuilder.Entity<CandidateVacancy>(entity =>
            {
                entity.HasKey(ca => ca.Id);
                entity.Property(r => r.CandidateId).IsRequired();
                entity.Property(r => r.VacancyId).IsRequired();
                entity.Property(r => r.CorrectAnswerCount).IsRequired();
                entity.Property(r => r.WrongAnswerCount).IsRequired();
                entity.Property(e => e.ResumeId);

                entity.HasOne(ca => ca.Candidate)
                    .WithMany(c => c.CandidateVacancies)
                    .HasForeignKey(ca => ca.CandidateId);

                entity.HasOne(ca => ca.Vacancy)
                    .WithMany(ao => ao.CandidateVacancies)
                    .HasForeignKey(ca => ca.VacancyId);
                entity.HasOne(c => c.Resume)
                    .WithMany(r => r.CandidateVacancy)
                    .HasForeignKey(r => r.ResumeId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<Domain.Entities.File>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Path).IsRequired();
                entity.Property(r => r.FileName).IsRequired();
                entity.Property(e => e.Extension).IsRequired();
                entity.Property(e => e.FileType).IsRequired();
                entity.Property(e => e.MimeType).IsRequired();
                entity.Property(e => e.Size).HasPrecision(10, 2);
                entity.Property(e => e.OriginalFileName).IsRequired();
            });

            modelBuilder.Entity<VwVacancy>()
              .ToView("VwVacancy")
              .HasNoKey();

            modelBuilder.Entity<VwCandidate>()
              .ToView("VwCandidate")
              .HasNoKey().Property(e => e.Percentage)
              .HasColumnType("decimal(10, 2)");

            modelBuilder.Entity<VwCandidateAnswers>()
              .ToView("VwCandidateAnswers")
              .HasNoKey();
        }
    }
}
