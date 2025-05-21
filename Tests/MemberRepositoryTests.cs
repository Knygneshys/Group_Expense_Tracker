using backend.Data;
using backend.Data.Entities;
using backend.Data.Repositories;
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

        [Fact]
        public async Task RemoveAsync_RemoveMemberOfId2_GroupReturnedByFindByAsyncShouldNotContainMemberOfId2()
        {
            // Arrange
            using var context = CreateContext();
            var memberRepo = new MemberRepository(context);
            var groupRepo = new GroupRepository(context);
            int id = 2;
            Member member = await memberRepo.FindByIdAsync(id);
            // Act
            await memberRepo.RemoveAsync(id);

            // Assert
            Group group = await groupRepo.FindByIdAsync(1);
            Assert.False(group.Members.Contains(member));
        }

        [Fact]
        public async Task RemoveAsync_RemoveMemberOfId2_FindMemberByIdAsyncShouldReturnNull()
        {
            // Arrange
            using var context = CreateContext();
            var memberRepo = new MemberRepository(context);
            int id = 2;
            // Act
            await memberRepo.RemoveAsync(id);

            // Assert
            Member actual = await memberRepo.FindByIdAsync(id);
            Assert.Null(actual);
        }

        [Fact]
        public async Task RemoveAsync_RemoveMemberOfId2_GetAllMembersShouldReturn7MembersInsteadOf8()
        {
            // Arrange
            using var context = CreateContext();
            var memberRepo = new MemberRepository(context);
            int id = 2;
            Member member = await memberRepo.FindByIdAsync(id);
            // Act
            await memberRepo.RemoveAsync(id);

            // Assert
            List<Member> members = await memberRepo.GetAll();
            Assert.Equal(7, members.Count);
        }

        [Fact]
        public async Task RemoveAsync_RemoveMemberOfId2_GroupsGetAllShouldReturnGroup1With2MembersInsteadOf3()
        {
            // Arrange
            using var context = CreateContext();
            var memberRepo = new MemberRepository(context);
            var groupRepo = new GroupRepository(context);
            int id = 2;
            Member member = await memberRepo.FindByIdAsync(id);
            // Act
            await memberRepo.RemoveAsync(id);

            // Assert
            List<Group> groups = await groupRepo.GetAll();
            Assert.False(groups[0].Members.Contains(member));
        }

        [Fact]
        public async Task GetAllByGroupID_GetAllMembersFromGroup2()
        {
            // Arrange
            using var context = CreateContext();
            var memberRepo = new MemberRepository(context);
            int groupId = 2;
            Member mem1 = new Member(4, "Mike", "Brown", 100.0m, 2);
            Member mem2 = new Member(5, "Sarah", "Wilson", -60.0m, 2);
            // Act
            var members = await memberRepo.GetAllByGroupId(groupId);

            // Assert
            Assert.Equal(2, members.Count);
            Assert.True(members.Contains(mem1));
            Assert.True(members.Contains(mem2));
        }
    }
}
