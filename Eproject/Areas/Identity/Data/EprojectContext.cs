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
    public EprojectContext(DbContextOptions<EprojectContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
