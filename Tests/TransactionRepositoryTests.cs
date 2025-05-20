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
            var member10 = new Member(10, "New", "Blood", -5.79f, 3);

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
            Transaction transaction = new Transaction(3, 10, 159.99f, 'E', new List<TransactionRecipient>
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
            Assert.Equal(30.0f, recipient6.Debt);
            Assert.Equal(0, recipient7.Debt);
            Assert.Equal(15.0f, recipient8.Debt);
            Assert.Equal(-5.79f, recipient10.Debt, 2);
        }

        [Fact]
        public async Task CreateAsync_CreatesNewDynamicTransactionInGroup1_Group1sMemberOfId2ShouldHaveReducedDebt()
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

        [Fact]
        public async Task CreateAsync_CreatesNewDynamicTransactionInGroup1_Group1sMemberOfId2ShouldHaveDebtOf0()
        {
            // Arrange
            using var context = CreateContext();
            var transRepo = new TransactionRepository(context);
            var memRepo = new MemberRepository(context);
            Transaction transaction = new Transaction(1, 3, 40.53f, 'D', new List<TransactionRecipient>
            {
                new TransactionRecipient(2, 30.3f),
                new TransactionRecipient(9, 10.23f)
            });

            // Act
            var actual = await transRepo.CreateAsync(transaction);

            // Assert
            Member recipient1 = await memRepo.FindByIdAsync(2);
            Member recipient2 = await memRepo.FindByIdAsync(9);
            Assert.NotNull(actual);
            Assert.Equal(0f, recipient1.Debt, 2);
            Assert.Equal(-40.11f, recipient2.Debt, 2);
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

            // Arrange
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
            Transaction transaction = new Transaction(2, 0, 100, 'P', new List<TransactionRecipient>
            {
                new TransactionRecipient(4, 40),
                new TransactionRecipient(5, 60),
            });
            // Arrange

            var actual = await transRepo.CreateAsync(transaction);

            // Assert
            Member recipient4 = await memRepo.FindByIdAsync(4);
            Member recipient5 = await memRepo.FindByIdAsync(5);
            Assert.Equal(100f, recipient4.Debt, 2);
            Assert.Equal(0, recipient5.Debt, 2);
        }
    }
}
