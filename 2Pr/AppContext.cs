using Microsoft.EntityFrameworkCore;

public class CompanyContext : DbContext
{
    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()  // для lazy loading
            .UseSqlite("Data Source=company.db"); // лёгкая локальная БД
    }
}
