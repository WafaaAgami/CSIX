using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CSIX.Data;

namespace CSIX.Data;

public class CSIXContext : IdentityDbContext<CSIXUser>
{
    public CSIXContext(DbContextOptions<CSIXContext> options)
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

    public DbSet<CSIX.Data.Tasks> Tasks { get; set; } = default!;

    public DbSet<CSIX.Data.Activities> Activities { get; set; } = default!;
}
