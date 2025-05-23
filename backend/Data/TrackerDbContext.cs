﻿using Backend.Data.Entities;
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
                new Member(1, "John", "Doe", 50.0m, 1),
                new Member(2, "Alice", "Smith", -25.5m, 1),
                new Member(3, "Bob", "Johnson", 75.5m, 1),

                new Member(4, "Mike", "Brown", 100.0m, 2),
                new Member(5, "Sarah", "Wilson", -60.0m, 2),

                new Member(6, "Emma", "Davis", 30.0m, 3),
                new Member(7, "James", "Miller", -47.3m, 3),
                new Member(8, "Olivia", "Taylor", 15.0m, 3)
                );
            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { Id = 1, GroupId = 1, SenderId = 0, Amount = 90m, SplitType = 'E' },
                new Transaction { Id = 2, GroupId = 1, SenderId = 1, Amount = 200m, SplitType = 'P' },
                new Transaction { Id = 3, GroupId = 1, SenderId = 3, Amount = 100m, SplitType = 'D' },
                new Transaction { Id = 4, GroupId = 2, SenderId = 0, Amount = 100m, SplitType = 'E' },
                new Transaction { Id = 5, GroupId = 2, SenderId = 5, Amount = 100m, SplitType = 'D' }
            );
            modelBuilder.Entity<TransactionRecipient>().HasData(
                new { Id = 1, RecipientId = 1, Payment = 30m, TransactionId = 1 },
                new { Id = 2, RecipientId = 2, Payment = 30m, TransactionId = 1 },
                new { Id = 3, RecipientId = 3, Payment = 30m, TransactionId = 1 },

                new { Id = 4, RecipientId = 1, Payment = 50m, TransactionId = 2 },
                new { Id = 5, RecipientId = 2, Payment = 30m, TransactionId = 2 },
                new { Id = 6, RecipientId = 3, Payment = 20m, TransactionId = 2 },

                new { Id = 7, RecipientId = 1, Payment = 50m, TransactionId = 3 },
                new { Id = 8, RecipientId = 2, Payment = 25m, TransactionId = 3 },
                new { Id = 9, RecipientId = 3, Payment = 25m, TransactionId = 3 },

                new { Id = 10, RecipientId = 4, Payment = 50m, TransactionId = 4 },
                new { Id = 11, RecipientId = 5, Payment = 50m, TransactionId = 4 },

                new { Id = 12, RecipientId = 4, Payment = 70m, TransactionId = 5 },
                new { Id = 13, RecipientId = 5, Payment = 30m, TransactionId = 5 }
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
