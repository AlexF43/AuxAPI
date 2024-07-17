using AuxAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuxAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<TestItem> TestItems { get; set; }
}