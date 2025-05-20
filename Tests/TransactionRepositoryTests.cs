using backend.Data.Entities;
using backend.Data.Repositories;
using Backend.Data.Entities;
using Backend.Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class TransactionRepositoryTests : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<TrackerDbContext> _contextOptions;
        private List<Group> groupList;
        private List<Member> memberList;

        public TransactionRepositoryTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<TrackerDbContext>()
                 .UseSqlite(_connection)
                 .Options;

            using var context = new TrackerDbContext(_contextOptions);
            context.Database.EnsureCreated();
            var group1 = new Group(1, "Walle");
            var group2 = new Group(2, "Darbininkai");
            var group3 = new Group(3, "Bites");

            var member1 = new Member(1, "John", "Doe", 50.0f, 1);
            member1.Group = group1;
            var member2 = new Member(2, "Alice", "Smith", -25.5f, 1);
            member2.Group = group1;
            var member3 = new Member(3, "Bob", "Johnson", 75.5f, 1);
            member3.Group = group1;
            var member4 = new Member(4, "Mike", "Brown", 100.0f, 2);
            var member5 = new Member(5, "Sarah", "Wilson", -60.0f, 2);
            var member6 = new Member(6, "Emma", "Davis", 30.0f, 3);
            var member7 = new Member(7, "James", "Miller", -47.3f, 3);
            var member8 = new Member(8, "Olivia", "Taylor", 15.0f, 3);

            groupList = [
                group1,
                group2,
                group3
                ];

            memberList = [
                member1,
                member2,
                member3,

                member4,
                member5,

                member6,
                member7,
                member8
                ];

            context.SaveChanges();
        }
        private TrackerDbContext CreateContext() => new TrackerDbContext(_contextOptions);

        public void Dispose()
        {
            _connection.Dispose();
        }

        [Fact]
        public async Task CreateAsync_CreatesNewDynamicTransactionInGroup1_Group1sMemberOfId2Should()
        {
            // Arrange
            using var context = CreateContext();
            var transRepo = new TransactionRepository(context);
            var memRepo = new MemberRepository(context);
            Transaction transaction = new Transaction(1, 3, 20.3f, 'D', new List<TransactionRecipient>
            {
                new TransactionRecipient(2, 20.3f)
            });

            // Act
            var actual = await transRepo.CreateAsync(transaction);

            // Assert
            Member recipient = await memRepo.FindByIdAsync(2);
            Assert.NotNull(actual);
            Assert.Equal(-5.2, recipient.Debt);
        }
    }
}
