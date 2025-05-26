using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quiz.Models;

namespace ExamSystem.Models
{
    public class ExamContext :IdentityDbContext<ApplicationUser>
    {

        public ExamContext(DbContextOptions<ExamContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
        modelBuilder.Entity<Answer>()
        .HasOne(a => a.Result)
        .WithMany(r => r.Answers)
        .HasForeignKey(a => a.ResultId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Exam>()
       .HasOne(e => e.user)
       .WithMany(u => u.Exams)
       .HasForeignKey(e => e.UserId)
       .OnDelete(DeleteBehavior.Restrict);
        }


        public DbSet<Questions> Questions { set; get; }
        public DbSet<Exam> exams { set; get; }
        public DbSet<Result> results { set; get; }

        public DbSet<Answer> answers { set; get; }



    }
}
