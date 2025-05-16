using Group_Expense_Tracker.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Group_Expense_Tracker.Server.Data
{
    public class TrackerDbContext(DbContextOptions<TrackerDbContext> options) : DbContext(options)
    {
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasData(
                new Group(1, "Walle"),
                new Group(2, "Darbininkai"),
                new Group(3, "Bitės")
                );
        }
    }
}
