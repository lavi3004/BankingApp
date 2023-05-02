using BankingApp.Areas.Identity.Data;
using BankingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Data;

public class BankingAppContext : IdentityDbContext<User>
{
    public BankingAppContext(DbContextOptions<BankingAppContext> options)
        : base(options)
    {
    }

    public DbSet<Transaction>? Transactions { get; set; }
    public DbSet<BankAccount>? BankAccounts { get; set; }
    public DbSet<Card>? Cards { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(50);
        builder.Property(u => u.LastName).HasMaxLength(50);
        builder.Property(u => u.Adress).HasMaxLength(50);
        builder.Property(u => u.PhoneNumber).HasMaxLength(25);
    }
}
