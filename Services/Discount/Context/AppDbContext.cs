using System.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    public DbSet<Coupon> Coupons { get; set; }
    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);

}