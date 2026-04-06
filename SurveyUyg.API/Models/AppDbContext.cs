using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SurveyUyg.API.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> QuestionOptions { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Response>(entity => {
                entity.Property(e => e.SubmittedAt).HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<Answer>(entity => {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                entity.HasOne(a => a.Question)
                    .WithMany(q => q.Answers)
                    .HasForeignKey(a => a.QuestionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.SelectedOption)
                    .WithMany(o => o.Answers)
                    .HasForeignKey(a => a.SelectedOptionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}