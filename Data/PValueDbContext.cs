using Microsoft.EntityFrameworkCore;
using PValue.Models;

namespace PValue.Data
{
    public class PValueDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=CNS02MABDSK01D\\SQL2017;Initial Catalog=OPPE_P_VALUE_AUTO;Integrated Security=true");
        }

        public DbSet<Indicator_12> Indicator_12 { get; set; }
        public DbSet<StatsTable> StatsTable { get; set; }
    }
}