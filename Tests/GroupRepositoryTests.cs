using backend.Data;
using backend.Data.Entities;
using backend.Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Threading.Tasks;


namespace Tests
{
    public class GroupRepositoryTests : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<TrackerDbContext> _contextOptions;
        private List<Group> groupList;
        private List<Member> memberList;

        public GroupRepositoryTests()
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


            var member1 = new Member(1, "John", "Doe", 50.0m, 1);
            member1.Group = group1;
            var member2 = new Member(2, "Alice", "Smith", -25.5m, 1);
            member2.Group = group1;
            var member3 = new Member(3, "Bob", "Johnson", 75.5m, 1);
            member3.Group = group1;
            var member4 = new Member(4, "Mike", "Brown", 100.0m, 2);
            var member5 = new Member(5, "Sarah", "Wilson", -60.0m, 2);
            var member6 = new Member(6, "Emma", "Davis", 30.0m, 3);
            var member7 = new Member(7, "James", "Miller", -47.3m, 3);
            var member8 = new Member(8, "Olivia", "Taylor", 15.0m, 3);

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
        public async Task FindByIdAsync_Id1WasPassed_ShouldReturnFirstGroup()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new GroupRepository(context);

            // Act
            var group = await repository.FindByIdAsync(1);

            Assert.NotNull(group);
            Assert.Equal(groupList[0].Name, group.Name);
            Assert.Equal(memberList.Take(3).OrderByDescending(mem => mem.Debt), group.Members);
        }

        [Fact]
        public async Task FindByIdAsync_NonExistantIdWasPassed_ShouldReturnNull()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new GroupRepository(context);

            // Act
            var group = await repository.FindByIdAsync(100);

            // Assert
            Assert.Null(group);
        }

        [Fact]
        public async Task CreateAsync_Create4thGroup_ThereShouldBe4Groups()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new GroupRepository(context);
            Group expected = new Group(5, "Naujokai");

            // Act
            await repository.CreateAsync(expected);

            // Assert
            List<Group> groups = await repository.GetAll();
            Assert.Equal(5, groups.Count());
            Assert.Contains(expected, groups);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(-1, false)]
        [InlineData(100, false)]
        public void GroupExists(int id, bool expected)
        {
            // Arrange
            using var context = CreateContext();
            var repository = new GroupRepository(context);
            // Act
            bool actual = repository.GroupExists(id);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task RemoveAsync_PassedId4_ShouldRemove4thGroup()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new GroupRepository(context);
            // Act
            var actual = await repository.RemoveAsync(4);
            // Assert
            List<Group> groups = await repository.GetAll();
            Assert.Equal(4, groups.Count);
            Assert.DoesNotContain(actual, groups);
        }
        [Fact]
        public async Task RemoveAsync_PassedId1_ShouldNotRemoveGroupBecauseItHasMembers()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new GroupRepository(context);
            // Act
            var actual = await repository.RemoveAsync(1);
            // Assert
            List<Group> groups = await repository.GetAll();
            Assert.Equal(4, groups.Count);
            Assert.Null(actual);
        }

        [Fact]
        public async Task UpdateAsync_Update2ndGroup_2ndGroupsNameShouldBeChanged()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new GroupRepository(context);
            int groupId = 2;
            string newName = "Atnaujinta";
            Group secondGroup = await repository.FindByIdAsync(groupId);
            Group newGroup = new Group(newName);
            newGroup.Members = secondGroup.Members;
            // Act
            await repository.UpdateAsync(groupId, newGroup);
            // Assert
            secondGroup = await repository.FindByIdAsync(groupId);
            Assert.Equal(newName, secondGroup.Name);
        }
        [Theory]
        [InlineData(1, 100)]
        [InlineData(2, 40)]
        [InlineData(3, -2.3)]
        public async Task GetGroupDebt_GetFirstGroupsDebt_ShouldReturn100(int id, decimal expected)
        {
            // Arrange
            using var context = CreateContext();
            var repository = new GroupRepository(context);
            // Act
            var actual = await repository.GetGroupDebt(id);
            // Assert
            Assert.Equal(expected, actual);
        }
    }
}