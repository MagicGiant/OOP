using System.Data;
using BusinessLayer.Account;
using Microsoft.EntityFrameworkCore;

public class DataBaseContext : DbContext
{
    public DataBaseContext()
    {
        Database.EnsureCreated();
    }
    
    public DbSet<AccountData> AccountsData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-IIQJC96\\SQLEXPRESS; Database=TestDB; Trusted_Connection=True");
    }
}