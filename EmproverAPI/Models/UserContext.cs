using EmproverAPI.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace EmproverAPI.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Symbol> Symbols { get; set; }
        public DbSet<DayStatistics> DayStatistics { get; set; }
        public DbSet<Indicator> Indicators { get; set; }
        public DbSet<IndicatorParameter> IndicatorParameter { get; set; }
        public DbSet<AllowedValues> AllowedValues { get; set; }
    }
}
