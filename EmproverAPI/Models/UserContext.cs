using EmproverAPI.Models.DB;
using EmproverAPI.Models.Enum;

using Microsoft.EntityFrameworkCore;

namespace EmproverAPI.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User() { Name = "Sample", Description = "Sample user", Password = "password", Permissions = UserPermissionsEnum.User },
                new User() { Name = "Another", Description = "Another user", Password = "securePwd", Permissions = UserPermissionsEnum.Admin }
                );
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AllowedValues> AllowedValues { get; set; }
        public DbSet<DayStatistics> DayStatistics { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<FunctionParameter> FunctionParameters { get; set; }
        public DbSet<FunctionParameterValue> FunctionParameterValues { get; set; }
        public DbSet<Indicator> Indicators { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Symbol> Symbols { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
    }
}
