using Group_Expense_Tracker.Server.Data;
using Group_Expense_Tracker.Server.Data.Entities;
using Group_Expense_Tracker.Server.Data.Repositories;
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
    public class MemberRepositoryTests : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<TrackerDbContext> _contextOptions;

        public MemberRepositoryTests()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<TrackerDbContext>()
                 .UseSqlite(_connection)
                 .Options;

            using var context = new TrackerDbContext(_contextOptions);
            context.Database.EnsureCreated();

            context.SaveChanges();
        }

        private TrackerDbContext CreateContext() => new TrackerDbContext(_contextOptions);

        public void Dispose()
        {
            _connection.Dispose();
        }

        [Fact]
        public async Task CreateAsync_CreateNewMemberAndAddItToGroup1_MemberListShouldIncreaseByOneAndGroup1sMemberListShouldContainNewMember()
        {
            // Arrange
            using var context = CreateContext();
            var memberRepo = new MemberRepository(context);
            var groupRepo = new GroupRepository(context);
            Member newMember = new Member(9, "Naujas", "Naujokas", 90, 1);

            // Act
            await memberRepo.CreateAsync(newMember);

            // Assert
            List<Member> members = await memberRepo.GetAll();
            Group group = await groupRepo.FindByIdAsync(1);
            Assert.Equal(9, members.Count());
            Assert.Contains(newMember, members);
            Assert.Contains(newMember, group.Members);
        }
    }
}
