using AtoGobMx.Models;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Context
{
    public class AtoGobMxContext : DbContext
    {
        public DbSet<Areas> Area { get; set; }
        public AtoGobMxContext(DbContextOptions<AtoGobMxContext> options) : base(options) { }
    }
}
