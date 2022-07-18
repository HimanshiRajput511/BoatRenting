using Microsoft.EntityFrameworkCore;

namespace ModelLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<BoatRegister> BoatRegister { get; set; }
        public DbSet<RentingBusiness> RentingBusiness { get; set; }
    }
}
