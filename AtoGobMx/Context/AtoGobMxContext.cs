using AtoGobMx.Models;
using Microsoft.EntityFrameworkCore;

namespace AtoGobMx.Context
{
    public class AtoGobMxContext : DbContext
    {
        public AtoGobMxContext(DbContextOptions<AtoGobMxContext> options) : base(options) 
        { 
        }
        public DbSet<Areas> Area { get; set; }
        public DbSet<empleados> Empleados { get; set; }
    }
}
