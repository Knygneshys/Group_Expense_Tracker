using Group_Expense_Tracker.Server.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Group_Expense_Tracker.Server.Data
{
    public class TrackerDbContext(DbContextOptions<TrackerDbContext> options) : DbContext(options)
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasData(
                new Group(1, "Walle"),
                new Group(2, "Darbininkai"),
                new Group(3, "Bitės")
                );
            modelBuilder.Entity<Member>().HasData(
                new Member(1, "John", "Doe", 50.0f, 1),
                new Member(2, "Alice", "Smith", -25.5f, 1),
                new Member(3, "Bob", "Johnson", 75.0f, 1),

                new Member(4, "Mike", "Brown", 100.0f, 2),
                new Member(5, "Sarah", "Wilson", -60.0f, 2),

                new Member(6, "Emma", "Davis", 30.0f, 3),
                new Member(7, "James", "Miller", -45.0f, 3),
                new Member(8, "Olivia", "Taylor", 15.0f, 3)
                );

            modelBuilder.Entity<Member>()
                .HasOne(m => m.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(m => m.GroupId);
        }
    }
}
