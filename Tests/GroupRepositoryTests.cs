using System.Data.Common;
using System.Threading.Tasks;
using Group_Expense_Tracker.Server.Data;
using Group_Expense_Tracker.Server.Data.Entities;
using Group_Expense_Tracker.Server.Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;


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

            var member1 = new Member(1, "John", "Doe", 50.0f, 1);
            var member2 = new Member(2, "Alice", "Smith", -25.5f, 1);
            var member3 = new Member(3, "Bob", "Johnson", 75.0f, 1);
            var member4 = new Member(4, "Mike", "Brown", 100.0f, 2);
            var member5 = new Member(5, "Sarah", "Wilson", -60.0f, 2);
            var member6 = new Member(6, "Emma", "Davis", 30.0f, 3);
            var member7 = new Member(7, "James", "Miller", -45.0f, 3);
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
        public async Task FindByIdAsync_ShouldReturnFirstGroup()
        {
            // Arrange
            using var context = CreateContext();
            var repository = new GroupRepository(context);

            // Act
            var group = await repository.FindByIdAsync(1);

            Assert.NotNull(group);
            Assert.Equal(group.Name, groupList[0].Name);
        }
    }
}