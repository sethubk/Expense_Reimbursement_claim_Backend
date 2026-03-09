using Claim_Form.Entities;
using Microsoft.EntityFrameworkCore;

namespace Claim_Form.Data
{
    public class AppDbContext : DbContext

    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    
    }
}
