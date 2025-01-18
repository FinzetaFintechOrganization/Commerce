using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<CargoDetail> CargoDetails { get; set; }
    public DbSet<Company> Companies { get; set;}
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Operation> Operations { get; set; }
}