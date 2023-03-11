global using Microsoft.EntityFrameworkCore;

namespace EIF_VIKO_TVARKARASTIS_REST_API
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("host=localhost;database=viko;user id=postgres;password=root;");

        public DbSet<Teacher> Teachers => Set<Teacher>();
       
    }
}
