using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Entities
{
    public class TrackerDbContext(DbContextOptions<TrackerDbContext> options) : DbContext(options)
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionRecipient> TransactionRecipients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasData(
                new Group(1, "Walle"),
                new Group(2, "Darbininkai"),
                new Group(3, "Bitės"),
                new Group(4, "EmptyGroup")
                );
            modelBuilder.Entity<Member>().HasData(
                new Member(1, "John", "Doe", 50.0f, 1),
                new Member(2, "Alice", "Smith", -25.5f, 1),
                new Member(3, "Bob", "Johnson", 75.5f, 1),

                new Member(4, "Mike", "Brown", 100.0f, 2),
                new Member(5, "Sarah", "Wilson", -60.0f, 2),

                new Member(6, "Emma", "Davis", 30.0f, 3),
                new Member(7, "James", "Miller", -47.3f, 3),
                new Member(8, "Olivia", "Taylor", 15.0f, 3)
                );

            modelBuilder.Entity<Member>()
                .HasOne(m => m.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(m => m.GroupId);

            modelBuilder.Entity<Transaction>()
                .HasMany(t => t.Recipients)
                .WithOne(tr => tr.Transaction)
                .HasForeignKey(tr => tr.TransactionId);
        }
    }
}
