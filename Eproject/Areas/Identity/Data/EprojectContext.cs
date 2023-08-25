using Eproject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eproject.Data;

public class EprojectContext : IdentityDbContext<EprojectUser>
{
    public DbSet<FaqEntry> FaqEntries { get; set; }
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<AllowedRole> AllowedRoles { get; set; }
    public DbSet<SurveyEprojectUser> SurveyEprojectUsers { get; set; }
    public DbSet<SurveyCompletion> SurveyCompletions { get; set; }
    public DbSet<SupportInformation> SupportInformation { get; set; }
    public EprojectContext(DbContextOptions<EprojectContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<SurveyEprojectUser>()
            .HasKey(seu => new { seu.SurveyId, seu.EprojectUserId });

        builder.Entity<SurveyEprojectUser>()
            .HasOne(seu => seu.Survey)
            .WithMany(s => s.Participants)
            .HasForeignKey(seu => seu.SurveyId)
            .OnDelete(DeleteBehavior.Cascade); // Apply cascade delete

        builder.Entity<SurveyEprojectUser>()
            .HasOne(seu => seu.EprojectUser)
            .WithMany(u => u.ParticipatedSurveys)
            .HasForeignKey(seu => seu.EprojectUserId);

        builder.Entity<Survey>()
            .HasMany(s => s.Allowed)
            .WithOne(ar => ar.Survey) // Explicitly specify the navigation property
            .HasForeignKey(ar => ar.SurveyId) // Explicitly specify the foreign key
            .OnDelete(DeleteBehavior.Cascade); // Apply cascade delete

        builder.Entity<SurveyCompletion>()
            .HasKey(sc => new { sc.Id });

        builder.Entity<SurveyCompletion>()
            .HasOne(sc => sc.Survey)
            .WithMany(s => s.Completions)
            .HasForeignKey(sc => sc.SurveyId);

        builder.Entity<SurveyCompletion>()
            .HasOne(sc => sc.User)
            .WithMany(u => u.CompletedSurveys)
            .HasForeignKey(sc => sc.EprojectUserId);
    }
    // Add your customizations after calling base.OnModelCreating(builder);
}

