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

            var member9 = new Member(9, "Lala", "Land", -50.34f, 1);

            context.Members.Add(member9);

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
            Transaction transaction = new Transaction(1, 3, 30.53f, 'D', new List<TransactionRecipient>
            {
                new TransactionRecipient(2, 20.3f),
                new TransactionRecipient(9, 10.23f)
            });

            // Act
            var actual = await transRepo.CreateAsync(transaction);

            // Assert
            Member recipient1 = await memRepo.FindByIdAsync(2);
            Member recipient2 = await memRepo.FindByIdAsync(9);
            Assert.NotNull(actual);
            Assert.Equal(-5.2f, recipient1.Debt, 2);
            Assert.Equal(-40.11f, recipient2.Debt, 2);
        }
    }
}
