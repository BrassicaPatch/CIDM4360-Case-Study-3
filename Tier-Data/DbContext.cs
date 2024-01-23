using Microsoft.EntityFrameworkCore;
using Mailroom.DataTier.Models;

namespace Mailroom.DataTier;

public class AppDbContext : DbContext {
    public AppDbContext() : base() { }
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("DataSource=database.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Resident>(entity => {
            entity.HasKey(r => r.ID);

            entity.HasMany(r=>r.Packages)
                .WithOne();
        });

        modelBuilder.Entity<Package>(entity => {
            entity.HasKey(p => p.ID);

            entity.HasOne<Resident>()
                .WithMany(r => r.Packages);
        });
    }

    public DbSet<StaffUser> Staff {get; set;} = null!;
    public DbSet<Resident> Residents {get; set;} = null!;
    public DbSet<Package> Packages {get; set;} = null!;
}