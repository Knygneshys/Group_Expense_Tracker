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

            var member9 = new Member(9, "Lala", "Land", -50.34m, 1);
            var member10 = new Member(10, "New", "Blood", -5.79m, 3);

            context.Members.Add(member9);
            context.Members.Add(member10);

            context.SaveChanges();
        }
        private TrackerDbContext CreateContext() => new TrackerDbContext(_contextOptions);

        public void Dispose()
        {
            _connection.Dispose();
        }


        [Fact]
        public async Task CreateAsync_CreateNewEqualTransactionInGroup3_Group3sMemberOfId7ShouldHaveReducedDebtExceptTheSender()
        {
            // Arrange
            using var context = CreateContext();
            var transRepo = new TransactionRepository(context);
            var memRepo = new MemberRepository(context);
            Transaction transaction = new Transaction(6, 3, 10, 159.99m, 'E', new List<TransactionRecipient>
            {
                new TransactionRecipient(6),
                new TransactionRecipient(7),
                new TransactionRecipient(8),
                new TransactionRecipient(10)
            });

            // Act
            var actual = await transRepo.CreateAsync(transaction);

            // Assert
            Member recipient6 = await memRepo.FindByIdAsync(6);
            Member recipient7 = await memRepo.FindByIdAsync(7);
            Member recipient8 = await memRepo.FindByIdAsync(8);
            Member recipient10 = await memRepo.FindByIdAsync(10);
            Assert.NotNull(actual);
            Assert.Equal(30.0m, recipient6.Debt);
            Assert.Equal(0, recipient7.Debt);
            Assert.Equal(15.0m, recipient8.Debt);
            Assert.Equal(-5.79m, recipient10.Debt);
        }

        [Fact]
        public async Task CreateAsync_CreatesNewDynamicTransactionInGroup1_Group1sMemberOfId2ShouldHaveReducedDebt()
        {
            // Arrange
            using var context = CreateContext();
            var transRepo = new TransactionRepository(context);
            var memRepo = new MemberRepository(context);
            Transaction transaction = new Transaction(1, 3, 30.53m, 'D', new List<TransactionRecipient>
            {
                new TransactionRecipient(2, 20.3m),
                new TransactionRecipient(9, 10.23m)
            });

            // Act
            var actual = await transRepo.CreateAsync(transaction);

            // Assert
            Member recipient1 = await memRepo.FindByIdAsync(2);
            Member recipient2 = await memRepo.FindByIdAsync(9);
            Assert.NotNull(actual);
            Assert.Equal(-5.2m, recipient1.Debt);
            Assert.Equal(-40.11m, recipient2.Debt);
        }

        [Fact]
        public async Task CreateAsync_CreatesNewDynamicTransactionInGroup1_Group1sMemberOfId2ShouldHaveDebtOf0()
        {
            // Arrange
            using var context = CreateContext();
            var transRepo = new TransactionRepository(context);
            var memRepo = new MemberRepository(context);
            Transaction transaction = new Transaction(1, 3, 40.53m, 'D', new List<TransactionRecipient>
            {
                new TransactionRecipient(2, 30.3m),
                new TransactionRecipient(9, 10.23m)
            });

            // Act
            var actual = await transRepo.CreateAsync(transaction);

            // Assert
            Member recipient1 = await memRepo.FindByIdAsync(2);
            Member recipient2 = await memRepo.FindByIdAsync(9);
            Assert.NotNull(actual);
            Assert.Equal(0m, recipient1.Debt);
            Assert.Equal(-40.11m, recipient2.Debt);
        }


        [Fact]
        public async Task CreateAsync_CreatesNewPercentTransactionWherePercentagesDoNotAddUpTo100_ShouldReturnNull()
        {
            // Arrange
            using var context = CreateContext();
            var transRepo = new TransactionRepository(context);
            var memRepo = new MemberRepository(context);
            Transaction transaction = new Transaction(3, 0, 100, 'P', new List<TransactionRecipient>
            {
                new TransactionRecipient(6, 0),
                new TransactionRecipient(6, 7),
                new TransactionRecipient(6, 8)
            });

            // Act
            var actual = await transRepo.CreateAsync(transaction);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task CreateAsync_CreatesNewPercentTransactionInGroup2_Group2sMemberOfId5ShouldHaveReducedDebt()
        {
            // Arrange
            using var context = CreateContext();
            var transRepo = new TransactionRepository(context);
            var memRepo = new MemberRepository(context);
            Transaction transaction = new Transaction(2, 3, 100, 'P', new List<TransactionRecipient>
            {
                new TransactionRecipient(4, 40),
                new TransactionRecipient(5, 60),
            });
            // Act

            var actual = await transRepo.CreateAsync(transaction);

            // Assert
            Member recipient4 = await memRepo.FindByIdAsync(4);
            Member recipient5 = await memRepo.FindByIdAsync(5);
            Assert.Equal(100m, recipient4.Debt);
            Assert.Equal(0m, recipient5.Debt);
        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(2, 2)]
        public async Task GetAllFromGroup_GetsAllTransactionFromGroupWithGivenId_TransactionCountsShouldBeCorrect(int groupId, int expected)
        {
            // Arrange
            using var context = CreateContext();
            var transRepo = new TransactionRepository(context);
            var memRepo = new MemberRepository(context);

            // Act
            var actual = await transRepo.GetAllFromGroup(groupId);

            // Assert
            Assert.Equal(expected, actual.Count);
        }

    }
}
